using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace EzBiology.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                // TODO: Replace with real DB call
                gvStudentPerformance.DataSource = GetStudentPerformance();
                gvStudentPerformance.DataBind();
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
            return new List<StudentPerformanceRow>
            {
                new StudentPerformanceRow { StudentId = "S000001", Name = "Herbert II",       AvgQuiz = 87.6,  AvgAssess = 90  },
                new StudentPerformanceRow { StudentId = "S000002", Name = "John Hammer",      AvgQuiz = 69.5,  AvgAssess = 95  },
                new StudentPerformanceRow { StudentId = "S000003", Name = "Jack Hammer",      AvgQuiz = 74.3,  AvgAssess = 80  },
                new StudentPerformanceRow { StudentId = "S000004", Name = "Richard Lionheart",AvgQuiz = 100,   AvgAssess = 100 },
                new StudentPerformanceRow { StudentId = "S000005", Name = "Naoya",            AvgQuiz = 20,    AvgAssess = 10  },
                new StudentPerformanceRow { StudentId = "S000006", Name = "Herbert III",      AvgQuiz = 80,    AvgAssess = 80  },
                new StudentPerformanceRow { StudentId = "S000007", Name = "Gilbert",          AvgQuiz = 95.4,  AvgAssess = 88  },
                new StudentPerformanceRow { StudentId = "S000008", Name = "Thia",             AvgQuiz = 92.6,  AvgAssess = 95  },
                new StudentPerformanceRow { StudentId = "S000009", Name = "Alucard",          AvgQuiz = 55.1,  AvgAssess = 90  },
                new StudentPerformanceRow { StudentId = "S000010", Name = "Trevor Bel",       AvgQuiz = 73,    AvgAssess = 88  },
                new StudentPerformanceRow { StudentId = "S000011", Name = "Sypha Bel",        AvgQuiz = 85.6,  AvgAssess = 99  },
                new StudentPerformanceRow { StudentId = "S000012", Name = "Saint Germaine",   AvgQuiz = 96.2,  AvgAssess = 100 },
            };
        }
    }

    public class StudentPerformanceRow
    {
        public string StudentId { get; set; }
        public string Name      { get; set; }
        public double AvgQuiz   { get; set; }
        public double AvgAssess { get; set; }
    }
}
