using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EzBiologyy.Pages
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        private readonly string connStr =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private const string StatsQuery = @"
            SELECT
                COUNT(*) AS Total,
                SUM(CASE WHEN Role = 'Student'   THEN 1 ELSE 0 END) AS Students,
                SUM(CASE WHEN Role = 'Teacher'   THEN 1 ELSE 0 END) AS Teachers,
                SUM(CASE WHEN IsActive = 0       THEN 1 ELSE 0 END) AS Disabled
            FROM Users
            WHERE IsDeleted = 0;";

        private const string RecentQuery = @"
            SELECT TOP 5 UserID, Username, FullName, Role, IsActive
            FROM Users
            WHERE IsDeleted = 0
            ORDER BY UserID DESC;";

        private const string ToggleQuery     = "UPDATE Users SET IsActive = CASE WHEN IsActive=1 THEN 0 ELSE 1 END WHERE UserID = @id";
        private const string SoftDeleteQuery = "UPDATE Users SET IsDeleted = 1 WHERE UserID = @id";
        private const string CurrentAdminQuery = "SELECT UserID FROM Users WHERE Username = @username";

        public int CurrentAdminId { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || (Session["Role"] as string) != "Admin")
            {
                Response.Redirect("~/Pages/Login.aspx");
                return;
            }

            greetingLbl.Text = "Hello, Admin " + Session["Username"];
            CurrentAdminId = LookupCurrentAdminId();

            if (!IsPostBack)
            {
                LoadDashboard();
            }
        }

        private int LookupCurrentAdminId()
        {
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(CurrentAdminQuery, conn))
            {
                cmd.Parameters.AddWithValue("@username", Session["Username"].ToString());
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result == null ? 0 : Convert.ToInt32(result);
            }
        }

        private void LoadDashboard()
        {
            int total = 0, students = 0, teachers = 0, disabled = 0;
            DataTable recent = new DataTable();

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new SqlCommand(StatsQuery, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        total    = reader["Total"]    == DBNull.Value ? 0 : Convert.ToInt32(reader["Total"]);
                        students = reader["Students"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Students"]);
                        teachers = reader["Teachers"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Teachers"]);
                        disabled = reader["Disabled"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Disabled"]);
                    }
                }

                using (var cmd = new SqlCommand(RecentQuery, conn))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(recent);
                }
            }

            lblStatTotal.Text    = total.ToString();
            lblStatStudents.Text = students.ToString();
            lblStatTeachers.Text = teachers.ToString();
            lblStatDisabled.Text = disabled.ToString();

            lstRecent.DataSource = recent;
            lstRecent.DataBind();
            lblShowing.Text = string.Format("Showing {0} of {1} users", recent.Rows.Count, total);
        }

        protected void lstRecent_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int userId;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out userId)) return;

            string query;
            if (e.CommandName == "Delete")      query = SoftDeleteQuery;
            else if (e.CommandName == "Toggle") query = ToggleQuery;
            else                                return;

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadDashboard();
        }

        protected void Unnamed5_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminCreate.aspx");
        }
    }
}
