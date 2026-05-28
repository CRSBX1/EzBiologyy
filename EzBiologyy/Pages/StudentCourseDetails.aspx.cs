using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EzBiologyy.Pages
{
    public partial class StudentCourseDetails : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

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
                LoadCourseDetails();
                LoadMaterials();
                LoadQuizzes();
                LoadAssessments();
            }
        }

        private int GetCourseId()
        {
            if (Request.QueryString["CourseID"] == null)
            {
                Response.Redirect("StudentCourses.aspx");
                return 0;
            }

            int courseId;

            if (!int.TryParse(Request.QueryString["CourseID"], out courseId))
            {
                Response.Redirect("StudentCourses.aspx");
                return 0;
            }

            return courseId;
        }

        private void LoadCourseDetails()
        {
            int courseId = GetCourseId();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT CourseName
                    FROM Courses
                    WHERE CourseID = @CourseID;
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        lblCourseName.Text = result.ToString();
                        lblBreadcrumbCourseName.Text = result.ToString();
                    }
                    else
                    {
                        Response.Redirect("StudentCourses.aspx");
                    }
                }
            }
        }

        private void LoadMaterials()
        {
            int courseId = GetCourseId();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        lm.MaterialID,
                        lm.MaterialName,
                        lm.MaterialType,
                        lm.FilePath
                    FROM CourseMaterials cm
                    INNER JOIN LearningMaterials lm
                        ON cm.MaterialID = lm.MaterialID
                    WHERE cm.CourseID = @CourseID
                    ORDER BY lm.MaterialName;
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        rptMaterials.DataSource = dt;
                        rptMaterials.DataBind();

                        lblNoMaterials.Visible = dt.Rows.Count == 0;
                    }
                }
            }
        }

        private void LoadQuizzes()
        {
            int courseId = GetCourseId();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        a.AssessmentID,
                        a.AssessmentName,
                        a.AssessmentType
                    FROM CourseAssessments ca
                    INNER JOIN Assessments a
                        ON ca.AssessmentID = a.AssessmentID
                    WHERE ca.CourseID = @CourseID
                    AND a.AssessmentType = 'Quiz'
                    ORDER BY a.AssessmentName;
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        rptQuizzes.DataSource = dt;
                        rptQuizzes.DataBind();

                        lblNoQuizzes.Visible = dt.Rows.Count == 0;
                    }
                }
            }
        }

        private void LoadAssessments()
        {
            int courseId = GetCourseId();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        a.AssessmentID,
                        a.AssessmentName,
                        a.AssessmentType
                    FROM CourseAssessments ca
                    INNER JOIN Assessments a
                        ON ca.AssessmentID = a.AssessmentID
                    WHERE ca.CourseID = @CourseID
                    AND a.AssessmentType <> 'Quiz'
                    ORDER BY a.AssessmentName;
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        rptAssessments.DataSource = dt;
                        rptAssessments.DataBind();

                        lblNoAssessments.Visible = dt.Rows.Count == 0;
                    }
                }
            }
        }
    }
}