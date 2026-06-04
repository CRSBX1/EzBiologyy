using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace EzBiology.Pages
{
    public partial class Courses : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private string insertCourseQuery = "INSERT INTO Courses (CreatedByUserID, CourseName, CreatedAt) " +
            "OUTPUT INSERTED.CourseID " +
            " VALUES(@createdBy, @name, @created)";
        private string insertAssessmentsQuery = "INSERT INTO CourseAssessments (CourseID, AssessmentID) " +
            "VALUES(@cID, @aID)";
        private string insertMaterialsQuery = "INSERT INTO CourseMaterials (CourseID, MaterialID) " +
            "VALUES(@cID, @mID)";
        private string getAssessmentsQuery = "SELECT * FROM Assessments";
        private string getLearningMaterials = "SELECT * FROM LearningMaterials";
        private string checkQuery = "SELECT * FROM Courses WHERE CourseName=@name";

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
            string query = txtSearch.Text.Trim().ToLower();
            string category = ddlCategory.SelectedValue;
            var results = GetAllComponents()
                .Where(c => (c.Category == category || category=="All") &&
                            (string.IsNullOrEmpty(query) ||
                             c.Id.ToLower().Contains(query) ||
                             c.Name.ToLower().Contains(query)))
                .ToList();
            BindComponentTable(results);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var current = SelectedComponents;
            int addedCount = 0;

            foreach (GridViewRow row in gvComponents.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow)
                    continue;

                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");

                if (chkSelect != null && chkSelect.Checked)
                {
                    string id = gvComponents.DataKeys[row.RowIndex]["Id"].ToString();
                    string name = gvComponents.DataKeys[row.RowIndex]["Name"].ToString();
                    string category = gvComponents.DataKeys[row.RowIndex]["Category"].ToString();

                    if (!current.Any(x => x.Id == id && x.Category == category))
                    {
                        current.Add(new CourseComponent
                        {
                            Id = id,
                            Name = name,
                            Category = category
                        });

                        addedCount++;
                    }
                }
            }

            SelectedComponents = current;
            RefreshPanels();

            if (addedCount > 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = $"{addedCount} component(s) added successfully.";
            }
            else { 
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please select at least one component to add.";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            SelectedComponents = new List<CourseComponent>();
            BindComponentTable(GetAllComponents());
            RefreshPanels();
            lblMessage.Text = "";
            txtSearch.Text = "";
            ddlCategory.SelectedValue = "All";
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (SelectedComponents.Count == 0) {
                lblMessage.Text = "A minimum of one component must be added.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string courseName = courseNameTxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(courseName))
            {
                lblMessage.Text = "Please enter a course name.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            conn.Open();
            SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@name",courseName);
            object exists = checkCmd.ExecuteScalar();
            if (exists != null) {
                lblMessage.Text = "Course with the same name already exists, choose another name.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                conn.Close();
                return;
            }

            SqlCommand coursesCmd = new SqlCommand(insertCourseQuery, conn);
            coursesCmd.Parameters.AddWithValue("@createdBy", Session["UserID"]);
            coursesCmd.Parameters.AddWithValue("@name", courseName);
            coursesCmd.Parameters.AddWithValue("@created", DateTime.Now);
            int courseID = Convert.ToInt32(coursesCmd.ExecuteScalar()) ;

            SqlCommand assessCmd = new SqlCommand(insertAssessmentsQuery,conn);
            SqlCommand materialCmd = new SqlCommand(insertMaterialsQuery, conn);

            foreach (var comp in SelectedComponents)
            {
                assessCmd.Parameters.Clear();
                materialCmd.Parameters.Clear();
                if (comp.Category == "Quiz" || comp.Category == "Assessment")
                {
                    assessCmd.Parameters.AddWithValue("@cID", courseID); 
                    assessCmd.Parameters.AddWithValue("@aID", comp.Id);
                    assessCmd.ExecuteNonQuery();
                }
                else {
                    materialCmd.Parameters.AddWithValue("@cID", courseID); //@cID, @aID) and mID
                    materialCmd.Parameters.AddWithValue("@mID", comp.Id);
                    materialCmd.ExecuteNonQuery();
                }
            }
            conn.Close();
            SelectedComponents = new List<CourseComponent>();
            RefreshPanels();
            BindComponentTable(GetAllComponents());
            courseNameTxt.Text = "";
            txtSearch.Text = "";
            ddlCategory.SelectedValue = "All";
            lblMessage.Text = "Course created successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }

        protected void gvComponents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var comp = (CourseComponent)e.Row.DataItem;
            if (SelectedComponents.Any(x => x.Id == comp.Id && x.Category==comp.Category))
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
                .Where(c => c.Category == "Learning Materials").Select(c => c.Name).ToList();
            rptCourseMaterials.DataBind();

            var quizzes = selected.Where(c => c.Category == "Quiz").ToList();
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
            List<CourseComponent> courseComponents = new List<CourseComponent>();

            DataTable materialsTable = new DataTable();
            DataTable assessmentsTable = new DataTable();

            SqlCommand cmdLearn = new SqlCommand(getLearningMaterials, conn);
            SqlDataAdapter learnAdapter = new SqlDataAdapter(cmdLearn);
            learnAdapter.Fill(materialsTable);

            SqlCommand cmdAssess = new SqlCommand(getAssessmentsQuery, conn);
            SqlDataAdapter assessAdapter = new SqlDataAdapter(cmdAssess);
            assessAdapter.Fill(assessmentsTable);

            foreach (DataRow dr in materialsTable.Rows)
            {
                courseComponents.Add(new CourseComponent
                {
                    Id = dr["MaterialID"].ToString(),
                    Name = dr["MaterialName"].ToString(),
                    Category = "Learning Materials"
                });
            }

            foreach (DataRow dr in assessmentsTable.Rows)
            {
                string type = dr["AssessmentType"].ToString();

                courseComponents.Add(new CourseComponent
                {
                    Id = dr["AssessmentID"].ToString(),
                    Name = dr["AssessmentName"].ToString(),
                    Category = type == "quiz" ? "Quiz" : "Assessment"
                });
            }

            return courseComponents;
        }
    }

    [Serializable]
    public class CourseComponent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
