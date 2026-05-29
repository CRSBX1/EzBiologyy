using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace EzBiology.Pages
{
    public partial class Assessments : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private string insertQuery = "INSERT INTO Assessments(CreatedByUserID, AssessmentName," +
            "AssessmentType, CreatedAt) VALUES (@userID, @name, @type, @datetime)";
        private string retrieveIDQuery = "SELECT AssessmentID FROM Assessments WHERE AssessmentName = @name";
        private string insertQuestionQuery = "INSERT INTO Questions (AssessmentID, QuestionNumber, QuestionText, QuestionType) " +
            "OUTPUT INSERTED.QuestionID " + 
            "VALUES (@id, @qNum, @qTxt, @qType)";
        private string insertAnswerQuery = "INSERT INTO AnswerOptions (QuestionID, OptionLabel, OptionText, IsCorrect) " +
            "VALUES (@id, @oLabel, @oText, @correct)";
        

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
            SaveRepeaterStateToViewState();
            AssessMode = "quiz";
            BindAll();
        }

        protected void btnTabAssess_Click(object sender, EventArgs e)
        {
            SaveRepeaterStateToViewState();
            AssessMode = "assess";
            BindAll();
        }

        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            SaveRepeaterStateToViewState();

            var list = Questions;
            list.Add(new QuestionModel
            {
                Number = list.Count + 1,
                Text = "",
                QuestionType = "mc",
                Options = DefaultOptions()
            });

            Questions = list;
            BindAll();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Questions = DefaultQuestions();
            txtTitle.Text = "";
            lblMessage.Text = "";
            BindAll();
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            SaveRepeaterStateToViewState();

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                lblMessage.Text = "Please enter a title before publishing.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            conn.Open();
            SqlCommand checkCmd = new SqlCommand(retrieveIDQuery, conn);
            checkCmd.Parameters.AddWithValue("@name", txtTitle.Text.Trim());

            if(checkCmd.ExecuteScalar() != null)
            {
                lblMessage.Text = "An assessment with that title already exists." +
                    "Please choose a different title.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                conn.Close();
                return;
            }

            SqlCommand insertAssessmentCmd = new SqlCommand(insertQuery, conn);
            insertAssessmentCmd.Parameters.AddWithValue("@userID", Session["UserID"]);
            insertAssessmentCmd.Parameters.AddWithValue("@name", txtTitle.Text.Trim());
            insertAssessmentCmd.Parameters.AddWithValue("@type", AssessMode);
            insertAssessmentCmd.Parameters.AddWithValue("@datetime", DateTime.Now);
            
            insertAssessmentCmd.ExecuteNonQuery();

            SqlCommand insertQuestionsCmd = new SqlCommand(insertQuestionQuery, conn);
            int assessmentID = Convert.ToInt32(checkCmd.ExecuteScalar());

            SqlCommand insertAnswersCmd = new SqlCommand(insertAnswerQuery, conn);


            foreach (var q in Questions) {
                insertQuestionsCmd.Parameters.Clear();
                insertQuestionsCmd.Parameters.AddWithValue("@id", assessmentID);
                insertQuestionsCmd.Parameters.AddWithValue("@qNum", q.Number);
                insertQuestionsCmd.Parameters.AddWithValue("@qTxt", q.Text);
                insertQuestionsCmd.Parameters.AddWithValue("@qType", q.QuestionType);
                int questionID = Convert.ToInt32(insertQuestionsCmd.ExecuteScalar()) ;
                foreach (var a in q.Options) {
                    insertAnswersCmd.Parameters.Clear();
                    insertAnswersCmd.Parameters.AddWithValue("@id", questionID);
                    insertAnswersCmd.Parameters.AddWithValue("@oLabel",
                        String.IsNullOrEmpty(a.Label)? (object)DBNull.Value:a.Label );
                    insertAnswersCmd.Parameters.AddWithValue("@oText", a.Text);
                    if (q.QuestionType == "mc")
                    {
                        if (a.Label == q.CorrectAnswer)
                        {
                            insertAnswersCmd.Parameters.AddWithValue("@correct", 1);
                        }
                        else {
                            insertAnswersCmd.Parameters.AddWithValue("@correct", 0);
                        }
                    }
                    else if (q.QuestionType == "sa")
                    {
                        insertAnswersCmd.Parameters.AddWithValue("@correct", 1);
                    }
                    else { //QuestionType == tf
                        if (a.Text == q.TrueFalseAnswer)
                        {
                            insertAnswersCmd.Parameters.AddWithValue("@correct", 1);
                        }
                        else
                        {
                            insertAnswersCmd.Parameters.AddWithValue("@correct", 0);
                        }
                    }
                    insertAnswersCmd.ExecuteNonQuery();
                }
            }

            string type = AssessMode == "quiz" ? "Quiz" : "Assessment";
            lblMessage.Text = $"{type} '{txtTitle.Text}' published successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            conn.Close();
            Questions = DefaultQuestions();
            txtTitle.Text = "";
            BindAll();
        }

        protected void rptQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item &&
                e.Item.ItemType != ListItemType.AlternatingItem) return;

            var q = (QuestionModel)e.Item.DataItem;
            var rptOpts = (Repeater)e.Item.FindControl("rptOptions");
            var ddlType = (DropDownList)e.Item.FindControl("ddlType");
            var rblTrueFalse = (RadioButtonList)e.Item.FindControl("rblTrueFalse");
            var ddlCorrect = (DropDownList)e.Item.FindControl("ddlCorrect");
            var txtAnswer = (TextBox)e.Item.FindControl("txtSampleAnswer");

            // Bind answer options
            if (rptOpts != null)
            {
                rptOpts.DataSource = q.Options;
                rptOpts.DataBind();
            }

            // Restore selected question type after PostBack
            if (ddlType != null && !string.IsNullOrEmpty(q.QuestionType))
            {
                var item = ddlType.Items.FindByValue(q.QuestionType);
                if (item != null)
                    ddlType.SelectedValue = q.QuestionType;
            }

            if (ddlCorrect != null && !string.IsNullOrEmpty(q.CorrectAnswer))
            {
                var item = ddlCorrect.Items.FindByValue(q.CorrectAnswer);
                if (item != null)
                    ddlCorrect.SelectedValue = q.CorrectAnswer;
            }

            if (txtAnswer != null)
                txtAnswer.Text = q.SampleAnswer;

            if (rblTrueFalse != null && !string.IsNullOrEmpty(q.TrueFalseAnswer))
            {
                var item = rblTrueFalse.Items.FindByValue(q.TrueFalseAnswer);
                if (item != null)
                    rblTrueFalse.SelectedValue = q.TrueFalseAnswer;
            }
        }

        private void BindAll()
        {
            bool isQuiz = AssessMode == "quiz";
            litPageTitle.Text = isQuiz ? "Create Quiz" : "Create Assessment";
            lblTitleField.Text = isQuiz ? "Quiz Title" : "Assessment Title";
            btnTabQuiz.CssClass = isQuiz ? "tab-btn active-tab" : "tab-btn inactive-tab";
            btnTabAssess.CssClass = !isQuiz ? "tab-btn active-tab" : "tab-btn inactive-tab";
            rptQuestions.DataSource = Questions;
            rptQuestions.DataBind();
        }

        private static List<QuestionModel> DefaultQuestions() =>
            new List<QuestionModel>
            {
                new QuestionModel
                {
                    Number = 1,
                    Text = "Enter your question here",
                    QuestionType = "mc",
                    Options = DefaultOptions()
                }
            };

        private static List<AnswerOption> DefaultOptions() =>
            new List<AnswerOption>
            {
                new AnswerOption { Label="A", Text="Option 1"    },
                new AnswerOption { Label="B", Text="Option 2"   },
                new AnswerOption { Label="C", Text="Option 3"       },
                new AnswerOption { Label="D", Text="All of the above" },
            };

        private void SaveRepeaterStateToViewState()
        {
            var updatedQuestions = new List<QuestionModel>();

            foreach (RepeaterItem item in rptQuestions.Items)
            {
                if (item.ItemType != ListItemType.Item &&
                    item.ItemType != ListItemType.AlternatingItem)
                    continue;

                var ddlType = (DropDownList)item.FindControl("ddlType");
                var txtQuestion = (TextBox)item.FindControl("txtQuestion");
                var rptOptions = (Repeater)item.FindControl("rptOptions");
                var ddlCorrect = (DropDownList)item.FindControl("ddlCorrect");
                var txtSampleAnswer = (TextBox)item.FindControl("txtSampleAnswer");
                var rblTrueFalse = (RadioButtonList)item.FindControl("rblTrueFalse");

                var question = new QuestionModel
                {
                    Number = updatedQuestions.Count + 1,
                    Text = GetPostedValue(txtQuestion, ""),
                    QuestionType = GetPostedValue(ddlType, "mc"),
                    Options = new List<AnswerOption>(),
                    CorrectAnswer = GetPostedValue(ddlCorrect, ""),
                    SampleAnswer = GetPostedValue(txtSampleAnswer, ""),
                    TrueFalseAnswer = GetPostedValue(rblTrueFalse, "")
                };

                if (rptOptions != null)
                {
                    if (question.QuestionType == "mc")
                    {
                        foreach (RepeaterItem optItem in rptOptions.Items)
                        {
                            if (optItem.ItemType != ListItemType.Item &&
                                optItem.ItemType != ListItemType.AlternatingItem)
                                continue;

                            var txtOption = (TextBox)optItem.FindControl("txtOption");
                            question.Options.Add(new AnswerOption
                            {
                                Label = GetOptionLabel(question.Options.Count),
                                Text = txtOption?.Text ?? ""
                            });
                        }
                    }
                    else if (question.QuestionType == "sa") {
                        question.Options.Add(new AnswerOption
                        {
                            Label = String.Empty,
                            Text = question.SampleAnswer
                        });
                    }
                    else { //true false question
                        question.Options.Add(new AnswerOption
                        {
                            Label = String.Empty,
                            Text = "True"
                        });
                        question.Options.Add(new AnswerOption
                        {
                            Label = String.Empty,
                            Text = "False"
                        });
                    }
                    
                }

                updatedQuestions.Add(question);
            }

            Questions = updatedQuestions;
        }

        private string GetPostedValue(WebControl control, string defaultValue = "")
        {
            if (control == null)
                return defaultValue;

            string value = Request.Form[control.UniqueID];

            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        private string GetOptionLabel(int index)
        {
            return ((char)('A' + index)).ToString();
        }

    }

    [Serializable]
    public class QuestionModel
    {

        public int Number { get; set; }
        public string Text { get; set; }
        public string QuestionType { get; set; } = "mc";
        public List<AnswerOption> Options { get; set; } = new List<AnswerOption>();

        public string CorrectAnswer { get; set; }
        public string SampleAnswer { get; set; }
        public string TrueFalseAnswer { get; set; }
    }

    [Serializable]
    public class AnswerOption
    {
        public string Label { get; set; }
        public string Text { get; set; }
    }
}
