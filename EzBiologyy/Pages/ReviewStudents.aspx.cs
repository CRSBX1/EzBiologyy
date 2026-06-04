using EzBiologyy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EzBiology.Pages
{
    public partial class ReviewStudents : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private string getQuery =
            "SELECT UserID, FullName, " +
            "ROUND(AVG(CASE WHEN AssessmentType = 'quiz' THEN GradePoints END), 2) AS AverageQuizPoints, " +
            "ROUND(AVG(CASE WHEN AssessmentType = 'assess' THEN GradePoints END), 2) AS AverageAssessmentPoints " +
            "FROM Users INNER JOIN Grades ON Grades.StudentUserID=Users.UserID " +
            "INNER JOIN Assessments ON Grades.AssessmentID = Assessments.AssessmentID GROUP BY UserID, FullName;";
        private string getAssessmentsQuery = "SELECT UserID, Username, AssessmentName," +
            "AssessmentType, Grade, GradePoints from Assessments " +
            "INNER JOIN Grades ON Assessments.AssessmentID = Grades.AssessmentID " +
            "INNER JOIN Users ON Grades.StudentUserID = Users.UserID WHERE StudentUserID = @studentID;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack) { PopulateStudentDropdown(); RefreshPage(); }
        }

        protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e) => RefreshPage();
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e) => BindGradesTable();

        private void PopulateStudentDropdown()
        {
            ddlStudent.DataSource = GetStudents();
            ddlStudent.DataTextField = "Name";
            ddlStudent.DataValueField = "Id";
            ddlStudent.DataBind();
        }

        private void RefreshPage()
        {
            var s = GetStudentById(ddlStudent.SelectedValue);
            if (s == null) return;
            litGrade.Text = s.OverallGrade;
            litAvgQuiz.Text = s.AvgQuiz.ToString("0.##");
            litAvgAssess.Text = s.AvgAssess.ToString("0.##");
            BindGradesTable();
        }

        private void BindGradesTable()
        {
            var s = GetStudentById(ddlStudent.SelectedValue);
            var rows = s?.Grades.Where(g => g.Category == ddlCategory.SelectedValue).ToList()
                       ?? new List<GradeRow>();
            foreach (var row in rows) {
                System.Diagnostics.Debug.WriteLine(row.Name);
            }
            gvGrades.DataSource = rows;
            gvGrades.DataBind();
        }

        private StudentRecord GetStudentById(string id) =>
            GetStudents().FirstOrDefault(s => s.Id == id);

        private List<StudentRecord> GetStudents(){
            conn.Open();
            double overallGrade = 0;
            List<StudentRecord> students = new List<StudentRecord>();

            //Student overview query
            SqlCommand cmdOverview = new SqlCommand(getQuery, conn);
            DataTable dtOverview = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdOverview);
            adapter.Fill(dtOverview);

            //Student assessment details query
            SqlCommand cmdDetails = new SqlCommand(getAssessmentsQuery, conn);
            DataTable dtDetails = new DataTable();
            SqlDataAdapter adapterDetails = new SqlDataAdapter(cmdDetails);

            foreach (DataRow dr in dtOverview.Rows) {
                StudentRecord s = new StudentRecord();
                overallGrade = (Convert.ToDouble(dr["AverageQuizPoints"]) + Convert.ToDouble(dr["AverageAssessmentPoints"])) / 2;
                s.setID(dr["UserID"].ToString());
                s.setName(dr["FullName"].ToString());
                s.setOverallGrade(overallGrade);
                s.setAvgQuiz(Convert.ToDouble(dr["AverageQuizPoints"]));
                s.setAvgAssess(Convert.ToDouble(dr["AverageAssessmentPoints"]));
                //set the grades
                cmdDetails.Parameters.Clear();
                cmdDetails.Parameters.AddWithValue("@studentID", dr["UserID"].ToString());
                dtDetails.Clear();
                adapterDetails.Fill(dtDetails);
                s.Grades = dtDetails.AsEnumerable().Select(d => new GradeRow {
                    Name = d["AssessmentName"].ToString(),
                    Grade = d["Grade"].ToString(),
                    GradePoints = Convert.ToDouble(d["GradePoints"]),
                    Category = d["AssessmentType"].ToString()
                }).ToList();
                students.Add(s);
            }

            conn.Close();
            return students;
        }
    }

    public class StudentRecord
    {
        public void setID(string id) {Id = id;}
        public void setName(string name) {Name = name;}
        public void setOverallGrade(double grade) {GradePointsToLetterGrade(grade);}
        public void setAvgQuiz(double avgQuiz) {AvgQuiz = avgQuiz;}
        public void setAvgAssess(double avgAssess) {AvgAssess = avgAssess;}

        public string Id { get; set; }
        public string Name { get; set; }
        public string OverallGrade { get; set; }
        public double AvgQuiz { get; set; }
        public double AvgAssess { get; set; }
        public List<GradeRow> Grades { get; set; } = new List<GradeRow>();

        private void GradePointsToLetterGrade(double points){
            if (points >= 97) {
                OverallGrade = "A+";
            }
            else if (points >= 93) {
                OverallGrade = "A";
            }
            else if (points >= 90) {
                OverallGrade = "A-";
            }
            else if (points >= 87) {
                OverallGrade = "B+";
            }
            else if (points >= 83) {
                OverallGrade = "B";
            }
            else if (points >= 80) {
                OverallGrade = "B-";
            }
            else if (points >= 77) {
                OverallGrade = "C+";
            }
            else if (points >= 73) {
                OverallGrade = "C";
            }
            else if (points >= 70) {
                OverallGrade = "C-";
            }
            else if (points >= 67) {
                OverallGrade = "D+";
            }
            else if (points >= 60) {
                OverallGrade = "D";
            }
            else {
                OverallGrade = "F";
            }
        }
    }

    public class GradeRow
    {
        public string Name { get; set; }
        public string Grade { get; set; }
        public double GradePoints { get; set; }
        public string Category { get; set; }
    }
}
