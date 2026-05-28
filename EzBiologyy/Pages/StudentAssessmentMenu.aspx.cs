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
            if (!IsPostBack)
            {
                LoadAssessments();
            }
        }

        private void LoadAssessments()
        {
            int studentID =
            EzBiologyy.UserSession.StudentID;

            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"

SELECT
A.AssessmentID,
A.AssessmentName,
A.AssessmentType,
C.CourseName,

CASE

WHEN G.GradeID IS NOT NULL
THEN 'Submitted'

WHEN
(
    @StudentID=1002
    AND
    A.AssessmentName IN
    (
        'Genetics Worksheet',
        'Human Body Quiz',
        'Human Systems Assessment'
    )
)
THEN 'Upcoming'

ELSE 'Open'

END AS Status

FROM Assessments A

LEFT JOIN Courses C
ON A.CourseID=C.CourseID

LEFT JOIN Grades G
ON A.AssessmentID=G.AssessmentID
AND G.StudentUserID=@StudentID

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