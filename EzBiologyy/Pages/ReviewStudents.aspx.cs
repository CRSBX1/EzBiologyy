using System;
using System.Collections.Generic;
using System.Linq;

namespace EzBiology.Pages
{
    public partial class ReviewStudents : System.Web.UI.Page
    {
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
            ddlStudent.DataSource     = GetStudents();
            ddlStudent.DataTextField  = "Name";
            ddlStudent.DataValueField = "Id";
            ddlStudent.DataBind();
        }

        private void RefreshPage()
        {
            var s = GetStudentById(ddlStudent.SelectedValue);
            if (s == null) return;
            litGrade.Text     = s.OverallGrade;
            litAvgQuiz.Text   = s.AvgQuiz.ToString("0.##");
            litAvgAssess.Text = s.AvgAssess.ToString("0.##");
            BindGradesTable();
        }

        private void BindGradesTable()
        {
            var s    = GetStudentById(ddlStudent.SelectedValue);
            var rows = s?.Grades.Where(g => g.Category == ddlCategory.SelectedValue).ToList()
                       ?? new List<GradeRow>();
            gvGrades.DataSource = rows;
            gvGrades.DataBind();
        }

        private StudentRecord GetStudentById(string id) =>
            GetStudents().FirstOrDefault(s => s.Id == id);

        private List<StudentRecord> GetStudents() => SampleStudents();

        private static List<StudentRecord> SampleStudents()
        {
            var qt = new[] { "living organisms","Cell → Biosphere","Prokaryotic vs. eukaryotic cells","Organelles","Cell membrane structure and transport mechanisms","Carbohydrates, lipids, proteins, nucleic acids","Enzymes and their functions","Mitosis and Meiosis","DNA structure and replication","Basic inheritance patterns","Gene expression","Theory of evolution" };
            var at = new[] { "Cell Final Test", "Microorganisms Final Test" };

            GradeRow Q(string n, string g, double p) => new GradeRow { Name=n, Grade=g, GradePoints=p, Category="Quiz" };
            GradeRow A(string n, string g, double p) => new GradeRow { Name=n, Grade=g, GradePoints=p, Category="Assessment" };

            return new List<StudentRecord>
            {
                new StudentRecord { Id="S000004", Name="Richard Lionheart", OverallGrade="A+", AvgQuiz=99.99, AvgAssess=99.99, Grades=qt.Select(t=>Q(t,"A",100)).Concat(at.Select(t=>A(t,"A",100))).ToList() },
                new StudentRecord { Id="S000001", Name="Herbert II",        OverallGrade="A",  AvgQuiz=87.6,  AvgAssess=90,    Grades=qt.Select(t=>Q(t,"A",88)).Concat(at.Select(t=>A(t,"A",90))).ToList()  },
                new StudentRecord { Id="S000005", Name="Naoya",             OverallGrade="F",  AvgQuiz=20,    AvgAssess=10,    Grades=qt.Select(t=>Q(t,"F",20)).Concat(at.Select(t=>A(t,"F",10))).ToList()  },
                new StudentRecord { Id="S000007", Name="Gilbert",           OverallGrade="A",  AvgQuiz=95.4,  AvgAssess=88,    Grades=qt.Select(t=>Q(t,"A",95)).Concat(at.Select(t=>A(t,"A",88))).ToList()  },
                new StudentRecord { Id="S000008", Name="Thia",              OverallGrade="A",  AvgQuiz=92.6,  AvgAssess=95,    Grades=qt.Select(t=>Q(t,"A",93)).Concat(at.Select(t=>A(t,"A",95))).ToList()  },
            };
        }
    }

    public class StudentRecord
    {
        public string Id           { get; set; }
        public string Name         { get; set; }
        public string OverallGrade { get; set; }
        public double AvgQuiz      { get; set; }
        public double AvgAssess    { get; set; }
        public List<GradeRow> Grades { get; set; } = new List<GradeRow>();
    }

    public class GradeRow
    {
        public string Name        { get; set; }
        public string Grade       { get; set; }
        public double GradePoints { get; set; }
        public string Category    { get; set; }
    }
}
