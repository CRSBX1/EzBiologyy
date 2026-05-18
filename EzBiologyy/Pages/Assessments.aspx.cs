using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace EzBiology.Pages
{
    public partial class Assessments : System.Web.UI.Page
    {
        private string AssessMode
        {
            get => (string)(ViewState["AssessMode"] ?? "quiz");
            set => ViewState["AssessMode"] = value;
        }

        private List<QuestionModel> Questions
        {
            get => (List<QuestionModel>)(ViewState["Questions"] ?? DefaultQuestions());
            set => ViewState["Questions"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                ViewState["Questions"] = DefaultQuestions();
                BindAll();
            }
        }

        protected void btnTabQuiz_Click(object sender, EventArgs e)
        {
            AssessMode = "quiz";
            BindAll();
        }

        protected void btnTabAssess_Click(object sender, EventArgs e)
        {
            AssessMode = "assess";
            BindAll();
        }

        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            var list = Questions;
            list.Add(new QuestionModel { Number = list.Count + 1, Text = "", Options = DefaultOptions() });
            Questions = list;
            BindAll();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Questions       = DefaultQuestions();
            txtTitle.Text   = "";
            lblMessage.Text = "";
            BindAll();
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                lblMessage.Text      = "Please enter a title before publishing.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            // TODO: Collect questions from Repeater, build entity, save to DB
            string type = AssessMode == "quiz" ? "Quiz" : "Assessment";
            lblMessage.Text      = $"{type} '{txtTitle.Text}' published successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }

        protected void rptQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item &&
                e.Item.ItemType != ListItemType.AlternatingItem) return;

            var q       = (QuestionModel)e.Item.DataItem;
            var rptOpts = (Repeater)e.Item.FindControl("rptOptions");
            if (rptOpts != null) { rptOpts.DataSource = q.Options; rptOpts.DataBind(); }
        }

        private void BindAll()
        {
            bool isQuiz = AssessMode == "quiz";
            litPageTitle.Text      = isQuiz ? "Create Quiz"       : "Create Assessment";
            lblTitleField.Text     = isQuiz ? "Quiz Title"         : "Assessment Title";
            btnTabQuiz.CssClass    = isQuiz ? "tab-btn active-tab" : "tab-btn inactive-tab";
            btnTabAssess.CssClass  = !isQuiz ? "tab-btn active-tab" : "tab-btn inactive-tab";
            rptQuestions.DataSource = Questions;
            rptQuestions.DataBind();
        }

        private static List<QuestionModel> DefaultQuestions() =>
            new List<QuestionModel>
            {
                new QuestionModel { Number = 1, Text = "What's neuroscience?", Options = DefaultOptions() }
            };

        private static List<AnswerOption> DefaultOptions() =>
            new List<AnswerOption>
            {
                new AnswerOption { Label="A", Text="Brain science"    },
                new AnswerOption { Label="B", Text="Microorganisms"   },
                new AnswerOption { Label="C", Text="Brain fart"       },
                new AnswerOption { Label="D", Text="All of the above" },
            };
    }

    [Serializable]
    public class QuestionModel
    {
        public int    Number  { get; set; }
        public string Text    { get; set; }
        public List<AnswerOption> Options { get; set; } = new List<AnswerOption>();
    }

    [Serializable]
    public class AnswerOption
    {
        public string Label { get; set; }
        public string Text  { get; set; }
    }
}
