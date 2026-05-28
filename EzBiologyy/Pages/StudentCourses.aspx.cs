using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EzBiologyy.Pages
{
    public partial class StudentCourses : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Temporary test student ID.
        // Later replace this with Session["UserID"] after login is confirmed.
        private int studentId = 3;

        protected void Page_Load(object sender, EventArgs e)
        {
            // TEMPORARY TESTING ONLY
            // Login is handled by group leader.
            // if (Session["Username"] == null)
            // {
            //     Response.Redirect("Login.aspx");
            // }

            if (!IsPostBack)
            {
                LoadEnrolledCourses();
                LoadAvailableCourses();
            }
        }

        private void LoadEnrolledCourses()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT c.CourseID, c.CourseName
                    FROM Courses c
                    INNER JOIN CourseEnrollments ce 
                        ON c.CourseID = ce.CourseID
                    WHERE ce.StudentUserID = @StudentUserID
                    ORDER BY c.CourseName;
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StudentUserID", studentId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        rptEnrolledCourses.DataSource = dt;
                        rptEnrolledCourses.DataBind();
                    }
                }
            }
        }

        private void LoadAvailableCourses()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT c.CourseID, c.CourseName
                    FROM Courses c
                    WHERE c.CourseID NOT IN
                    (
                        SELECT ce.CourseID
                        FROM CourseEnrollments ce
                        WHERE ce.StudentUserID = @StudentUserID
                    )
                    ORDER BY c.CourseName;
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StudentUserID", studentId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        rptAvailableCourses.DataSource = dt;
                        rptAvailableCourses.DataBind();
                    }
                }
            }
        }

        protected void rptAvailableCourses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Enroll")
            {
                int courseId = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        IF NOT EXISTS
                        (
                            SELECT 1
                            FROM CourseEnrollments
                            WHERE CourseID = @CourseID
                            AND StudentUserID = @StudentUserID
                        )
                        BEGIN
                            INSERT INTO CourseEnrollments (CourseID, StudentUserID, EnrolledAt)
                            VALUES (@CourseID, @StudentUserID, GETDATE())
                        END
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", courseId);
                        cmd.Parameters.AddWithValue("@StudentUserID", studentId);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadEnrolledCourses();
                LoadAvailableCourses();
            }
        }
    }
}