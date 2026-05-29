using System;
using System.Configuration;
using System.Data.SqlClient;

namespace EzBiology.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private string passwordQuery = "SELECT Password FROM Users WHERE Username=@username";
        private string roleQuery = "SELECT Role FROM Users WHERE Username=@username";
        private string userIDQuery = "SELECT UserID FROM Users WHERE Username=@username";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            conn.Open();
            SqlCommand cmd = new SqlCommand(passwordQuery, conn);
            cmd.Parameters.AddWithValue("@username", username);
            object pwdResult = cmd.ExecuteScalar();
            if (pwdResult != null)
            {
                string dbPassword = pwdResult.ToString();
                bool isPassword = BCrypt.Net.BCrypt.Verify(password, dbPassword);
                if (isPassword)
                {
                    //get role
                    SqlCommand cmd2 = new SqlCommand(roleQuery, conn);
                    cmd2.Parameters.AddWithValue("@username", username);
                    string roleResult = cmd2.ExecuteScalar().ToString();

                    //get userID
                    SqlCommand cmd3 = new SqlCommand(userIDQuery, conn);
                    cmd3.Parameters.AddWithValue("@username", username);
                    string userIDResult = cmd3.ExecuteScalar().ToString();


                    Session["Role"] = roleResult;
                    Session["Username"] = username;
                    Session["UserID"] = userIDResult;

                    switch (roleResult)
                    {
                        case "Admin":
                            Response.Redirect("AdminDashboard.aspx");
                            break;
                        case "Student":
                            Response.Redirect("StudentDashboard.aspx");
                            break;
                        case "Teacher":
                            Response.Redirect("Home.aspx");
                            break;
                    }
                }
            }
            errorMessage.Visible = true;
            //Response.Redirect("~/Pages/Home.aspx");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }
    }
}
