using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace EzBiology.Pages
{
    public partial class Courses : System.Web.UI.Page
    {
        private List<CourseComponent> SelectedComponents
        {
            get => (List<CourseComponent>)(ViewState["SelectedComponents"] ?? new List<CourseComponent>());
            set => ViewState["SelectedComponents"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                BindComponentTable(GetAllComponents());
                RefreshPanels();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string query    = txtSearch.Text.Trim().ToLower();
            string category = ddlCategory.SelectedValue;
            var results = GetAllComponents()
                .Where(c => c.Category == category &&
                            (string.IsNullOrEmpty(query) ||
                             c.Id.ToLower().Contains(query) ||
                             c.Name.ToLower().Contains(query)))
                .ToList();
            BindComponentTable(results);
            lblMessage.Text = "";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var current  = SelectedComponents;
            var category = ddlCategory.SelectedValue;
            foreach (var item in GetAllComponents().Where(c => c.Category == category))
                if (!current.Any(x => x.Id == item.Id))
                    current.Add(item);
            SelectedComponents = current;
            RefreshPanels();
            lblMessage.Text = "Components added successfully.";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            SelectedComponents = new List<CourseComponent>();
            BindComponentTable(GetAllComponents());
            RefreshPanels();
            lblMessage.Text = "";
            txtSearch.Text  = "";
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            // TODO: Save SelectedComponents to DB as a new Course entity
            lblMessage.Text = "Course created successfully!";
        }

        protected void gvComponents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var comp = (CourseComponent)e.Row.DataItem;
            if (SelectedComponents.Any(x => x.Id == comp.Id))
                e.Row.CssClass = "hl";
        }

        private void BindComponentTable(List<CourseComponent> data)
        {
            gvComponents.DataSource = data;
            gvComponents.DataBind();
        }

        private void RefreshPanels()
        {
            var selected = SelectedComponents;
            rptCourseMaterials.DataSource = selected
                .Where(c => c.Category == "CourseMaterials").Select(c => c.Name).ToList();
            rptCourseMaterials.DataBind();

            var quizzes     = selected.Where(c => c.Category == "Quiz").ToList();
            var assessments = selected.Where(c => c.Category == "Assessment").ToList();
            var sb = new System.Text.StringBuilder();
            if (quizzes.Any())
            {
                sb.Append("<p>Quizzes:</p><ul>");
                quizzes.ForEach(q => sb.AppendFormat("<li>{0}</li>", q.Name));
                sb.Append("</ul>");
            }
            if (assessments.Any())
            {
                sb.Append("<p>Assessments:</p><ul>");
                assessments.ForEach(a => sb.AppendFormat("<li>{0}</li>", a.Name));
                sb.Append("</ul>");
            }
            litQA.Text = sb.ToString();
        }

        private List<CourseComponent> GetAllComponents()
        {
            return new List<CourseComponent>
            {
                new CourseComponent { Id="LM0023", Name="Characteristics of living organisms",                  Category="CourseMaterials" },
                new CourseComponent { Id="LM0524", Name="Levels of biological organization (cell → biosphere)", Category="CourseMaterials" },
                new CourseComponent { Id="LM0086", Name="Prokaryotic vs. eukaryotic cells",                     Category="CourseMaterials" },
                new CourseComponent { Id="LM0122", Name="Organelles",                                           Category="CourseMaterials" },
                new CourseComponent { Id="LM0128", Name="Cell membrane structure and transport mechanisms",      Category="CourseMaterials" },
                new CourseComponent { Id="LM0265", Name="Carbohydrates, lipids, proteins, nucleic acids",       Category="CourseMaterials" },
                new CourseComponent { Id="LM0347", Name="Enzymes and their functions",                          Category="CourseMaterials" },
                new CourseComponent { Id="LM0287", Name="Mitosis and Meiosis",                                  Category="CourseMaterials" },
                new CourseComponent { Id="LM0311", Name="DNA structure and replication",                        Category="CourseMaterials" },
                new CourseComponent { Id="LM0032", Name="Basic inheritance patterns",                           Category="CourseMaterials" },
                new CourseComponent { Id="LM0033", Name="Gene expression",                                      Category="CourseMaterials" },
                new CourseComponent { Id="LM0434", Name="Theory of evolution",                                  Category="CourseMaterials" },
                new CourseComponent { Id="QZ0001", Name="Cells and their behaviors",                            Category="Quiz" },
                new CourseComponent { Id="AS0001", Name="Cell Final Test",                                      Category="Assessment" },
                new CourseComponent { Id="AS0002", Name="Microorganisms Final Test",                            Category="Assessment" },
            };
        }
    }

    public class CourseComponent
    {
        public string Id       { get; set; }
        public string Name     { get; set; }
        public string Category { get; set; }
    }
}
