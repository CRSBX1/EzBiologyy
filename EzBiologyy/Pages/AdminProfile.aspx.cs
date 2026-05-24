using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace EzBiologyy.Pages
{
    public partial class AdminProfile : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd;
        private string getQuery = "SELECT * FROM Users WHERE Username=@username";
        private string updateQuery = "UPDATE Users SET Username=@username,Age=@age,Gender=@gender WHERE UserID = @userID";
        private string updateQuery2 = "UPDATE Users SET Username=@username,Password=@password,Age=@age,Gender=@gender WHERE UserID = @userID";
        private string userID
        {
            get => (string)(ViewState["userID"] ?? string.Empty);
            set => ViewState["userID"] = value;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                conn.Open();
                cmd = new SqlCommand(getQuery, conn);
                cmd.Parameters.AddWithValue("@username", Session["Username"].ToString());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                userID = dt.Rows[0][0].ToString();
                username.Text = dt.Rows[0][1].ToString();
                gender.Text = dt.Rows[0][5].ToString();
                age.Text = dt.Rows[0][4].ToString();
                conn.Close();
            }
        }

        protected void Unnamed3_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = this.username.Text.Trim();
            string password = this.password.Text;
            string age = this.age.Text;
            string gender = this.gender.SelectedValue;

            try
            {
                conn.Open();
                cmd = new SqlCommand(getQuery, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@username", username);
                object result = cmd.ExecuteScalar();

                if (result != null && username != Session["Username"].ToString())
                {
                    lblMessage.Text = "This username already exists, pick another username";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    conn.Close();
                    return;
                }

                if (ddlPwd.SelectedValue == "Yes")
                {
                    cmd = new SqlCommand(updateQuery2, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(password));
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.ExecuteNonQuery();
                }
                lblMessage.Text = "Profile credentials successfully updated";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            finally
            {
                Session["Username"] = username;
                conn.Close();
            }
        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            username.Text = string.Empty;
            password.Text = string.Empty;
            confirmPassword.Text = string.Empty;
        }
    }
}