using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EzBiologyy.Pages
{
    public partial class AdminManage : System.Web.UI.Page
    {
        private readonly string connStr =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private const string SelectQuery = @"
            SELECT UserID, Username, FullName, Age, LTRIM(RTRIM(Gender)) AS Gender, Role, IsActive
            FROM Users
            WHERE IsDeleted = 0
              AND (@search = ''
                   OR FullName LIKE '%' + @search + '%'
                   OR Username LIKE '%' + @search + '%'
                   OR 'U' + RIGHT('00000' + CAST(UserID AS varchar(10)), 5) LIKE '%' + @search + '%')
              AND (@role = 'All Roles' OR Role = @role)
              AND (@status = 'All Status'
                   OR (@status = 'Active'   AND IsActive = 1)
                   OR (@status = 'Disabled' AND IsActive = 0))
            ORDER BY UserID;";

        private const string CountLiveQuery = "SELECT COUNT(*) FROM Users WHERE IsDeleted = 0";
        private const string ToggleQuery    = "UPDATE Users SET IsActive = CASE WHEN IsActive=1 THEN 0 ELSE 1 END WHERE UserID = @id";
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

            CurrentAdminId = LookupCurrentAdminId();

            if (!IsPostBack)
            {
                LoadUsers("", "All Roles", "All Status");
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

        private void LoadUsers(string search, string role, string status)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                var dt = new DataTable();
                using (var cmd = new SqlCommand(SelectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@search", search ?? "");
                    cmd.Parameters.AddWithValue("@role",   role   ?? "All Roles");
                    cmd.Parameters.AddWithValue("@status", status ?? "All Status");

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }

                int totalLive;
                using (var countCmd = new SqlCommand(CountLiveQuery, conn))
                {
                    totalLive = (int)countCmd.ExecuteScalar();
                }

                lstUsers.DataSource = dt;
                lstUsers.DataBind();
                lblShowing.Text = string.Format("Showing {0} of {1} users", dt.Rows.Count, totalLive);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUsers(searchInput.Text.Trim(), roleFilter.SelectedValue, statusFilter.SelectedValue);
        }

        protected void lstUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
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

            LoadUsers(searchInput.Text.Trim(), roleFilter.SelectedValue, statusFilter.SelectedValue);
        }

        protected void Unnamed7_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminCreate.aspx");
        }
    }
}
