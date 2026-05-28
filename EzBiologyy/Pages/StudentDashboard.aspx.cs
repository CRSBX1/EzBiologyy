using System;
using System.Configuration;
using System.Data.SqlClient;

namespace EzBiologyy.Pages
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        string connectionString =
        ConfigurationManager.ConnectionStrings["ConnectionString"]
        .ConnectionString;

        protected void Page_Load(
        object sender,
        EventArgs e)
        {
            // TEST USER SWITCH
            Session["UserID"] = 1002;
            Session["Username"] = "student1";

            if (!IsPostBack)
            {
                UpdateCourseStatus();
                LoadStudentData();
            }
        }

        private void UpdateCourseStatus()
        {
            int studentID =
            Convert.ToInt32(
            Session["UserID"]);

            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"

                UPDATE CE
                SET Status=

                CASE

                WHEN
                (
                    SELECT COUNT(*)
                    FROM Assessments A
                    WHERE A.CourseID=CE.CourseID
                )

                =

                (
                    SELECT COUNT(*)

                    FROM Assessments A

                    INNER JOIN Grades G
                    ON A.AssessmentID=G.AssessmentID

                    WHERE
                    A.CourseID=CE.CourseID
                    AND G.StudentUserID=@StudentID
                )

                THEN 'Completed'

                ELSE 'In Progress'

                END

                FROM CourseEnrollments CE
                WHERE CE.StudentUserID=@StudentID";

                SqlCommand cmd =
                new SqlCommand(
                query,
                conn);

                cmd.Parameters.AddWithValue(
                "@StudentID",
                studentID);

                cmd.ExecuteNonQuery();
            }
        }

        private void LoadStudentData()
        {
            int studentID =
            Convert.ToInt32(
            Session["UserID"]);

            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {
                conn.Open();

                string nameQuery = @"

                SELECT FullName
                FROM Users
                WHERE UserID=@ID";

                SqlCommand cmdName =
                new SqlCommand(
                nameQuery,
                conn);

                cmdName.Parameters.AddWithValue(
                "@ID",
                studentID);

                object result =
                cmdName.ExecuteScalar();

                if (result != null)
                {
                    nameLbl.Text =
                    result.ToString();
                }


                string countQuery = @"

                SELECT COUNT(*)
                FROM CourseEnrollments
                WHERE StudentUserID=@ID";

                SqlCommand cmdCount =
                new SqlCommand(
                countQuery,
                conn);

                cmdCount.Parameters.AddWithValue(
                "@ID",
                studentID);

                courseCountLbl.Text =
                cmdCount.ExecuteScalar()
                .ToString();


                string courseQuery = @"

                SELECT
                C.CourseName,
                CE.Status

                FROM CourseEnrollments CE

                INNER JOIN Courses C
                ON CE.CourseID=C.CourseID

                WHERE CE.StudentUserID=@ID";

                SqlCommand cmdCourse =
                new SqlCommand(
                courseQuery,
                conn);

                cmdCourse.Parameters.AddWithValue(
                "@ID",
                studentID);

                SqlDataReader reader =
                cmdCourse.ExecuteReader();

                courseRepeater.DataSource =
                reader;

                courseRepeater.DataBind();
            }
        }
    }
}