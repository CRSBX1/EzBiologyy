using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EzBiologyy.Pages
{
    public partial class AdminEdit : System.Web.UI.Page
    {
        private readonly string connStr =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private const string SelectQuery = @"
            SELECT UserID, Username, FullName, Age, LTRIM(RTRIM(Gender)) AS Gender, Role, IsActive
            FROM Users
            WHERE UserID = @id AND IsDeleted = 0";

        private const string UniqueQuery =
            "SELECT 1 FROM Users WHERE Username = @username AND UserID <> @id";

        private const string UpdateQuery = @"
            UPDATE Users
            SET Username = @username,
                FullName = @fullName,
                Age      = @age,
                Gender   = @gender,
                Role     = @role,
                IsActive = @active
            WHERE UserID = @id";

        private const string UpdateWithPasswordQuery = @"
            UPDATE Users
            SET Username = @username,
                Password = @password,
                FullName = @fullName,
                Age      = @age,
                Gender   = @gender,
                Role     = @role,
                IsActive = @active
            WHERE UserID = @id";

        private const string CurrentAdminQuery = "SELECT UserID FROM Users WHERE Username = @username";

        private int CurrentUserId
        {
            get { return (int)(ViewState["CurrentUserId"] ?? 0); }
            set { ViewState["CurrentUserId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || (Session["Role"] as string) != "Admin")
            {
                Response.Redirect("~/Pages/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int id;
                if (!int.TryParse(Request.QueryString["UserID"], out id) || id <= 0)
                {
                    Response.Redirect("AdminManage.aspx");
                    return;
                }

                if (!LoadUser(id))
                {
                    Response.Redirect("AdminManage.aspx");
                    return;
                }
            }
        }

        private bool LoadUser(int id)
        {
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(SelectQuery, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                var dt = new DataTable();
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }

                if (dt.Rows.Count == 0) return false;

                DataRow row = dt.Rows[0];
                CurrentUserId = (int)row["UserID"];

                fullName.Text = row["FullName"].ToString();
                username.Text = row["Username"].ToString();
                age.Text      = row["Age"] == DBNull.Value ? "" : row["Age"].ToString();

                string g = row["Gender"] == DBNull.Value ? "" : row["Gender"].ToString();
                if (gender.Items.FindByValue(g) != null) gender.SelectedValue = g;
                else gender.SelectedIndex = 0;

                string role = row["Role"].ToString();
                hfRole.Value   = role;
                hfActive.Value = Convert.ToBoolean(row["IsActive"]) ? "1" : "0";

                lblUserId.Text   = string.Format("U{0:D5}", CurrentUserId);
                lblUserName.Text = row["FullName"].ToString();
                lblUserRole.Text = role;

                return true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentUserId <= 0)
            {
                Response.Redirect("AdminManage.aspx");
                return;
            }

            string fullNameTxt = fullName.Text.Trim();
            string usernameTxt = username.Text.Trim();
            string passwordTxt = password.Text;
            string confirmTxt  = confirmPassword.Text;
            string ageTxt      = age.Text.Trim();
            string genderTxt   = gender.SelectedValue;
            string roleTxt     = hfRole.Value;
            bool   active      = hfActive.Value == "1";

            if (string.IsNullOrWhiteSpace(fullNameTxt) ||
                string.IsNullOrWhiteSpace(usernameTxt) ||
                string.IsNullOrWhiteSpace(ageTxt))
            {
                ShowMessage("Full Name, Username and Age are required.", error: true);
                return;
            }

            if (usernameTxt.Contains(" "))
            {
                ShowMessage("Username cannot contain spaces.", error: true);
                return;
            }

            int ageVal;
            if (!int.TryParse(ageTxt, out ageVal) || ageVal < 1 || ageVal > 120)
            {
                ShowMessage("Please enter a valid age between 1 and 120.", error: true);
                return;
            }

            if (roleTxt != "Admin" && roleTxt != "Student" && roleTxt != "Teacher")
            {
                ShowMessage("Please select a valid role.", error: true);
                return;
            }

            // Guard: prevent the logged-in admin from demoting or disabling themselves.
            int loggedInAdminId = LookupCurrentAdminId();
            if (CurrentUserId == loggedInAdminId)
            {
                if (roleTxt != "Admin")
                {
                    ShowMessage("You cannot change your own role away from Admin.", error: true);
                    return;
                }
                if (!active)
                {
                    ShowMessage("You cannot disable your own account.", error: true);
                    return;
                }
            }

            bool changingPassword = !string.IsNullOrEmpty(passwordTxt) || !string.IsNullOrEmpty(confirmTxt);
            if (changingPassword && passwordTxt != confirmTxt)
            {
                ShowMessage("Passwords do not match.", error: true);
                return;
            }

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (var probe = new SqlCommand(UniqueQuery, conn))
                {
                    probe.Parameters.AddWithValue("@username", usernameTxt);
                    probe.Parameters.AddWithValue("@id",       CurrentUserId);
                    if (probe.ExecuteScalar() != null)
                    {
                        ShowMessage("Username '" + usernameTxt + "' is already taken by another user.", error: true);
                        return;
                    }
                }

                string sql = changingPassword ? UpdateWithPasswordQuery : UpdateQuery;
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id",       CurrentUserId);
                    cmd.Parameters.AddWithValue("@username", usernameTxt);
                    cmd.Parameters.AddWithValue("@fullName", fullNameTxt);
                    cmd.Parameters.AddWithValue("@age",      ageVal);
                    cmd.Parameters.AddWithValue("@gender",   genderTxt);
                    cmd.Parameters.AddWithValue("@role",     roleTxt);
                    cmd.Parameters.AddWithValue("@active",   active ? 1 : 0);
                    if (changingPassword)
                    {
                        cmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(passwordTxt));
                    }
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("AdminManage.aspx");
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

        private void ShowMessage(string text, bool error)
        {
            lblMessage.Text = (error ? "⚠️ " : "✅ ") + text;
            lblMessage.ForeColor = error ? System.Drawing.Color.FromArgb(0xE5, 0x39, 0x35)
                                         : System.Drawing.Color.FromArgb(0x3B, 0x6D, 0x11);
        }
    }
}
