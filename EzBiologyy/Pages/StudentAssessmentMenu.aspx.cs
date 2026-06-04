using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EzBiologyy.Pages
{
    public partial class StudentAssessmentMenu : System.Web.UI.Page
    {
        string connectionString =
        ConfigurationManager.ConnectionStrings["ConnectionString"]
        .ConnectionString;

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
                LoadAssessments();
            }
        }

        private void LoadAssessments()
        {
            int studentID = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"

SELECT DISTINCT
A.AssessmentID,
A.AssessmentName,
A.AssessmentType,
STRING_AGG(C.CourseName, ', ') AS CourseName,

CASE
WHEN EXISTS
    (
        SELECT 1
        FROM Grades G
        WHERE G.AssessmentID = A.AssessmentID
        AND G.StudentUserID = @StudentID
    )
THEN 'Submitted'
ELSE 'Open'

END AS Status

FROM CourseAssessments CA

INNER JOIN CourseEnrollments CE
ON CA.CourseID=CE.CourseID

INNER JOIN Courses C
ON CA.CourseID=C.CourseID

INNER JOIN Assessments A
ON CA.AssessmentID=A.AssessmentID

WHERE CE.StudentUserID = @StudentID

GROUP BY
A.AssessmentID,
A.AssessmentName,
A.AssessmentType

ORDER BY A.AssessmentID";

                SqlCommand cmd =
                new SqlCommand(
                query,
                conn);

                cmd.Parameters.AddWithValue(
                "@StudentID",
                studentID);

                SqlDataAdapter da =
                new SqlDataAdapter(cmd);

                DataTable dt =
                new DataTable();

                da.Fill(dt);

                assessmentRepeater.DataSource =
                dt;

                assessmentRepeater.DataBind();
            }
        }
    }
}