using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EzBiologyy.Pages
{
    public partial class TakeQuiz : System.Web.UI.Page
    {
        string connectionString =
        ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public int TimerSeconds = 120;

        protected void Page_Load(
        object sender,
        EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadQuiz();
            }
        }

        private void LoadQuiz()
        {
            int assessmentID =
            Convert.ToInt32(
            Request.QueryString["AssessmentID"]);

            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {
                conn.Open();

                string titleQuery = @"
                SELECT
                AssessmentName,
                AssessmentType
                FROM Assessments
                WHERE AssessmentID=@ID";

                SqlCommand titleCmd =
                new SqlCommand(
                titleQuery,
                conn);

                titleCmd.Parameters.AddWithValue(
                "@ID",
                assessmentID);

                SqlDataReader reader =
                titleCmd.ExecuteReader();

                if (reader.Read())
                {
                    quizTitleLbl.Text =
                    reader["AssessmentName"]
                    .ToString();

                    string type =
                    reader["AssessmentType"]
                    .ToString();

                    TimerSeconds =
                    type == "Quiz"
                    ? 600
                    : 1200;
                }

                reader.Close();

                string questionQuery = @"
                SELECT *
                FROM Questions
                WHERE AssessmentID=@ID
                ORDER BY QuestionNumber";

                SqlCommand cmd =
                new SqlCommand(
                questionQuery,
                conn);

                cmd.Parameters.AddWithValue(
                "@ID",
                assessmentID);

                SqlDataAdapter da =
                new SqlDataAdapter(cmd);

                DataTable dt =
                new DataTable();

                da.Fill(dt);

                questionRepeater.DataSource =
                dt;

                questionRepeater.DataBind();
            }
        }

        protected void questionRepeater_ItemDataBound(
        object sender,
        RepeaterItemEventArgs e)
        {
            if (
            e.Item.ItemType ==
            ListItemType.Item ||

            e.Item.ItemType ==
            ListItemType.AlternatingItem)
            {
                HiddenField typeHidden =
                (HiddenField)
                e.Item.FindControl(
                "questionTypeHidden");

                string questionType =
                typeHidden.Value.Trim();

                Panel mcqPanel =
                (Panel)
                e.Item.FindControl(
                "mcqPanel");

                Panel fillPanel =
                (Panel)
                e.Item.FindControl(
                "fillPanel");

                HiddenField questionHidden =
                (HiddenField)
                e.Item.FindControl(
                "questionIDHidden");

                int questionID =
                Convert.ToInt32(
                questionHidden.Value);


                if (questionType == "sa")
                {
                    mcqPanel.Visible = false;
                    fillPanel.Visible = true;
                }
                else
                {
                    mcqPanel.Visible = true;
                    fillPanel.Visible = false;

                    RadioButtonList list =
                    (RadioButtonList)
                    e.Item.FindControl(
                    "optionList");

                    using (SqlConnection conn =
                    new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = @"
                        SELECT OptionText
                        FROM AnswerOptions
                        WHERE QuestionID=@ID";

                        SqlCommand cmd =
                        new SqlCommand(
                        query,
                        conn);

                        cmd.Parameters.AddWithValue(
                        "@ID",
                        questionID);

                        SqlDataReader reader =
                        cmd.ExecuteReader();

                        list.DataSource =
                        reader;

                        list.DataTextField =
                        "OptionText";

                        list.DataValueField =
                        "OptionText";

                        list.DataBind();
                    }
                }
            }
        }

        protected void submitBtn_Click(
        object sender,
        EventArgs e)
        {
            int studentID =
            Convert.ToInt32(Session["UserID"]);

            int assessmentID =
            Convert.ToInt32(
            Request.QueryString["AssessmentID"]);

            int totalQuestions = 0;
            int correctAnswers = 0;

            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (
                RepeaterItem item
                in questionRepeater.Items)
                {
                    HiddenField questionID =
                    (HiddenField)item.FindControl(
                    "questionIDHidden");

                    HiddenField type =
                    (HiddenField)item.FindControl(
                    "questionTypeHidden");

                    bool isCorrect = false;

                    totalQuestions++;

                    if (type.Value == "mc" || type.Value == "tf")
                    {
                        RadioButtonList list =
                        (RadioButtonList)item.FindControl(
                        "optionList");

                        if (list.SelectedItem != null)
                        {
                            string query = @"
                            SELECT IsCorrect
                            FROM AnswerOptions
                            WHERE QuestionID=@QuestionID
                            AND OptionText=@Answer";

                            SqlCommand cmd =
                            new SqlCommand(
                            query,
                            conn);

                            cmd.Parameters.AddWithValue(
                            "@QuestionID",
                            questionID.Value);

                            cmd.Parameters.AddWithValue(
                            "@Answer",
                            list.SelectedValue);

                            object result =
                            cmd.ExecuteScalar();

                            if (result != null)
                            {
                                isCorrect =
                                Convert.ToBoolean(
                                result);
                            }
                        }
                    }

                    else
                    {
                        TextBox txt =
                        (TextBox)item.FindControl(
                        "answerTextbox");

                        string query = @"
                        SELECT OptionText
                        FROM AnswerOptions
                        WHERE QuestionID=@ID
                        AND IsCorrect=1";

                        SqlCommand cmd =
                        new SqlCommand(
                        query,
                        conn);

                        cmd.Parameters.AddWithValue(
                        "@ID",
                        questionID.Value);

                        object result =
                        cmd.ExecuteScalar();

                        if (result != null)
                        {
                            isCorrect =
                            txt.Text.Trim()
                            .ToLower()
                            ==
                            result.ToString()
                            .Trim()
                            .ToLower();
                        }
                    }

                    if (isCorrect)
                    {
                        correctAnswers++;
                    }
                }

                int percentage =
                (correctAnswers * 100)
                /
                Math.Max(
                totalQuestions,
                1);

                string grade = "F-";

                if (percentage >= 80)
                    grade = "A+";

                else if (percentage >= 75)
                    grade = "A";

                else if (percentage >= 70)
                    grade = "B+";

                else if (percentage >= 65)
                    grade = "B";

                else if (percentage >= 60)
                    grade = "C+";

                else if (percentage >= 55)
                    grade = "C";

                else if (percentage >= 50)
                    grade = "C-";

                else if (percentage >= 40)
                    grade = "D";

                else if (percentage >= 30)
                    grade = "F+";

                else if (percentage >= 20)
                    grade = "F";

                string deleteQuery = @"
                DELETE FROM Grades
                WHERE AssessmentID=@AssessmentID
                AND StudentUserID=@StudentID";

                SqlCommand deleteCmd =
                new SqlCommand(
                deleteQuery,
                conn);

                deleteCmd.Parameters.AddWithValue(
                "@AssessmentID",
                assessmentID);

                deleteCmd.Parameters.AddWithValue(
                "@StudentID",
                studentID);

                deleteCmd.ExecuteNonQuery();

                string insertQuery = @"
                INSERT INTO Grades
                (
                AssessmentID,
                StudentUserID,
                Grade,
                GradePoints,
                GradedAt
                )
                VALUES
                (
                @AssessmentID,
                @StudentID,
                @Grade,
                @Points,
                GETDATE()
                )";

                SqlCommand insertCmd =
                new SqlCommand(
                insertQuery,
                conn);

                insertCmd.Parameters.AddWithValue(
                "@AssessmentID",
                assessmentID);

                insertCmd.Parameters.AddWithValue(
                "@StudentID",
                studentID);

                insertCmd.Parameters.AddWithValue(
                "@Grade",
                grade);

                insertCmd.Parameters.AddWithValue(
                "@Points",
                percentage);

                insertCmd.ExecuteNonQuery();
            }

            Session["Quiz1Submitted"] = true;

            Response.Redirect(
            "StudentAssessmentMenu.aspx");
        }
    }
}