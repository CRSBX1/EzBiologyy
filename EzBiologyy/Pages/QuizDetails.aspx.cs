using System;
using System.Configuration;
using System.Data.SqlClient;

namespace EzBiologyy.Pages
{
    public partial class QuizDetails : System.Web.UI.Page
    {
        string connectionString =
        ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadQuizInfo();
            }
        }

        private void LoadQuizInfo()
        {
            int assessmentID =
            Convert.ToInt32(
            Request.QueryString["AssessmentID"]);

            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                SELECT
                A.AssessmentName,
                C.CourseName
                FROM Assessments A
                LEFT JOIN Courses C
                ON A.AssessmentID=C.CourseID
                WHERE A.AssessmentID=@ID";

                SqlCommand cmd =
                new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue(
                "@ID",
                assessmentID);

                SqlDataReader reader =
                cmd.ExecuteReader();

                if (reader.Read())
                {
                    quizTitleLbl.Text =
                    reader["AssessmentName"].ToString();

                    courseLbl.Text =
                    reader["CourseName"].ToString();
                }
            }
        }

        protected void startBtn_Click(object sender, EventArgs e)
        {
            int assessmentID =
            Convert.ToInt32(
            Request.QueryString["AssessmentID"]);

            Response.Redirect(
            "TakeQuiz.aspx?AssessmentID="
            + assessmentID);
        }
    }
}