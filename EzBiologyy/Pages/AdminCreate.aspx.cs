using System;
using System.Configuration;
using System.Data.SqlClient;

namespace EzBiologyy.Pages
{
    public partial class AdminCreate : System.Web.UI.Page
    {
        private readonly string connStr =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private const string UniqueQuery = "SELECT 1 FROM Users WHERE Username = @username";
        private const string InsertQuery = @"
            INSERT INTO Users (Username, Password, FullName, Age, Gender, Role, IsActive, IsDeleted)
            VALUES (@username, @password, @fullName, @age, @gender, @role, 1, 0);";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || (Session["Role"] as string) != "Admin")
            {
                Response.Redirect("~/Pages/Login.aspx");
                return;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string fullNameTxt = fullName.Text.Trim();
            string usernameTxt = username.Text.Trim();
            string passwordTxt = password.Text;
            string confirmTxt  = confirmPassword.Text;
            string ageTxt      = age.Text.Trim();
            string genderTxt   = gender.SelectedValue;
            string roleTxt     = hfRole.Value;

            if (string.IsNullOrWhiteSpace(fullNameTxt) ||
                string.IsNullOrWhiteSpace(usernameTxt) ||
                string.IsNullOrWhiteSpace(passwordTxt) ||
                string.IsNullOrWhiteSpace(confirmTxt)  ||
                string.IsNullOrWhiteSpace(ageTxt))
            {
                ShowMessage("Please fill in all fields before creating a user.", error: true);
                return;
            }

            if (usernameTxt.Contains(" "))
            {
                ShowMessage("Username cannot contain spaces.", error: true);
                return;
            }

            if (passwordTxt != confirmTxt)
            {
                ShowMessage("Passwords do not match.", error: true);
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

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (var probe = new SqlCommand(UniqueQuery, conn))
                {
                    probe.Parameters.AddWithValue("@username", usernameTxt);
                    if (probe.ExecuteScalar() != null)
                    {
                        ShowMessage("Username '" + usernameTxt + "' is already taken. Please choose another.", error: true);
                        return;
                    }
                }

                using (var insert = new SqlCommand(InsertQuery, conn))
                {
                    insert.Parameters.AddWithValue("@username", usernameTxt);
                    insert.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(passwordTxt));
                    insert.Parameters.AddWithValue("@fullName", fullNameTxt);
                    insert.Parameters.AddWithValue("@age",      ageVal);
                    insert.Parameters.AddWithValue("@gender",   genderTxt);
                    insert.Parameters.AddWithValue("@role",     roleTxt);
                    insert.ExecuteNonQuery();
                }
            }

            Response.Redirect("AdminManage.aspx");
        }

        private void ShowMessage(string text, bool error)
        {
            lblMessage.Text = (error ? "⚠️ " : "✅ ") + text;
            lblMessage.ForeColor = error ? System.Drawing.Color.FromArgb(0xE5, 0x39, 0x35)
                                         : System.Drawing.Color.FromArgb(0x3B, 0x6D, 0x11);
        }
    }
}
