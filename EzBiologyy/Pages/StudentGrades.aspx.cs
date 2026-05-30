using System;
using System.Configuration;
using System.Data.SqlClient;

namespace EzBiologyy.Pages
{
    public partial class StudentGrades : System.Web.UI.Page
    {
        string connectionString =
        ConfigurationManager.ConnectionStrings["ConnectionString"]
        .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadGrades();
            }
        }

        private void LoadGrades()
        {
            int studentID =
            EzBiologyy.UserSession.StudentID;

            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                SELECT
                A.AssessmentName,
                G.Grade,
                G.GradePoints
                FROM Grades G
                INNER JOIN Assessments A
                ON G.AssessmentID=A.AssessmentID
                WHERE G.StudentUserID=@StudentID";

                SqlCommand cmd =
                new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue(
                "@StudentID",
                studentID);

                SqlDataReader reader =
                cmd.ExecuteReader();

                gradesGrid.DataSource = reader;
                gradesGrid.DataBind();
            }
        }
    }
}