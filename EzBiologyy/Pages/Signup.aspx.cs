using System;
using System.Configuration;
using System.Data.SqlClient;

namespace EzBiology.Pages
{
    public partial class Signup : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private string insertQuery = "INSERT INTO Users (Username, Password, Role, FullName, IsActive, IsDeleted) VALUES (@username, @password, @role, @name, @active, @deleted)";
        private string checkQuery = "SELECT * FROM Users WHERE Username = @username";

        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string fullName = txtFullName.Text.Trim();
            string role = rolePick.Text;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            conn.Open();
            SqlCommand cmd = new SqlCommand(checkQuery, conn);
            cmd.Parameters.AddWithValue("@username", username);
            object found = cmd.ExecuteScalar();

            if (found == null)
            {
                SqlCommand cmd2 = new SqlCommand(insertQuery, conn);
                cmd2.Parameters.AddWithValue("@username", username);
                cmd2.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(password));
                cmd2.Parameters.AddWithValue("@role", role);
                cmd2.Parameters.AddWithValue("@name", fullName);
                cmd2.Parameters.AddWithValue("@active", 1);
                cmd2.Parameters.AddWithValue("@deleted", 0);
                cmd2.ExecuteNonQuery();
                Response.Redirect("Login.aspx");
            }
            else
            {
                errMessage.Visible = true;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtFullName.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            rolePick.Text = string.Empty;
        }
    }
}
