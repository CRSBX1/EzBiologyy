using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EzBiologyy.Pages
{
    public partial class StudentCourses : System.Web.UI.Page
    {
        string connectionString =
        ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Username"] = "student1";

            if (!IsPostBack)
            {
                LoadCourses();
            }
        }

        private void LoadCourses()
        {
            string username = Session["Username"].ToString();

            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                SELECT C.CourseName
                FROM Courses C
                INNER JOIN CourseEnrollments CE
                ON C.CourseID=CE.CourseID
                INNER JOIN Users U
                ON U.UserID=CE.StudentUserID
                WHERE U.Username=@Username";

                SqlCommand cmd =
                new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue(
                    "@Username",
                    username);

                SqlDataReader reader =
                cmd.ExecuteReader();

                courseRepeater.DataSource = reader;
                courseRepeater.DataBind();
            }
        }
    }
}