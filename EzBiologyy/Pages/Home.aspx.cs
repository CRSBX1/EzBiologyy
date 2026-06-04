using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.UI.WebControls;

namespace EzBiology.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private string getQuery =
            "SELECT UserID, FullName, " +
            "ROUND(AVG(CASE WHEN AssessmentType = 'quiz' THEN GradePoints END), 2) AS AverageQuizPoints, " +
            "ROUND(AVG(CASE WHEN AssessmentType = 'assess' THEN GradePoints END), 2) AS AverageAssessmentPoints " +
            "FROM Users INNER JOIN Grades ON Grades.StudentUserID=Users.UserID " +
            "INNER JOIN Assessments ON Grades.AssessmentID = Assessments.AssessmentID GROUP BY UserID, FullName;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                gvStudentPerformance.DataSource = GetStudentPerformance();
                gvStudentPerformance.DataBind();
                labelTeacherName.Text = Session["Username"].ToString();
            }
        }

        protected void gvStudentPerformance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var row = (StudentPerformanceRow)e.Row.DataItem;
            if (row.AvgQuiz >= 95 && row.AvgAssess >= 95)
                e.Row.CssClass = "row-top";
            else if (row.AvgQuiz < 50 || row.AvgAssess < 50)
                e.Row.CssClass = "row-low";
        }

        private List<StudentPerformanceRow> GetStudentPerformance()
        {
            SqlCommand cmd = new SqlCommand(getQuery, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            List<StudentPerformanceRow> studentPerformanceRows = new List<StudentPerformanceRow>();
            foreach (DataRow dr in dt.Rows)
            {
                studentPerformanceRows.Add(new StudentPerformanceRow
                {
                    StudentId = dr["UserID"].ToString(),
                    Name = dr["FullName"].ToString(),
                    AvgQuiz = Convert.ToDouble(dr["AverageQuizPoints"]),
                    AvgAssess = Convert.ToDouble(dr["AverageAssessmentPoints"])
                });
            }
            return studentPerformanceRows;
        }
    }

    public class StudentPerformanceRow
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public double AvgQuiz { get; set; }
        public double AvgAssess { get; set; }
    }
}
