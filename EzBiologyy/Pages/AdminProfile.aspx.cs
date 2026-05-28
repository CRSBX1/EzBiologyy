using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EzBiologyy.Pages
{
    public partial class AdminProfile : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd;
        private string getQuery = "SELECT * FROM Users WHERE Username=@username";
        private string updateQuery = "UPDATE Users SET Username=@username,FullName=@fullName,Age=@age,Gender=@gender WHERE UserID = @userID";
        private string updateQuery2 = "UPDATE Users SET Username=@username,Password=@password,FullName=@fullName,Age=@age,Gender=@gender WHERE UserID = @userID";
        private string userID
        {
            get => (string)(ViewState["userID"] ?? string.Empty);
            set => ViewState["userID"] = value;
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
                conn.Open();
                cmd = new SqlCommand(getQuery, conn);
                cmd.Parameters.AddWithValue("@username", Session["Username"].ToString());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                userID = dt.Rows[0][0].ToString();
                username.Text = dt.Rows[0][1].ToString();
                fullName.Text = dt.Rows[0]["FullName"].ToString();
                string g = dt.Rows[0][5] == DBNull.Value ? "" : dt.Rows[0][5].ToString().Trim();
                if (gender.Items.FindByValue(g) != null) gender.SelectedValue = g;
                age.Text = dt.Rows[0][4].ToString();
                conn.Close();
            }
        }

        protected void Unnamed3_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = this.username.Text.Trim();
            string fullName = this.fullName.Text.Trim();
            string password = this.password.Text;
            string age = this.age.Text;
            string gender = this.gender.SelectedValue;

            if (string.IsNullOrWhiteSpace(fullName))
            {
                lblMessage.Text = "Full name is required";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

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
                    if (string.IsNullOrEmpty(password))
                    {
                        lblMessage.Text = "Please enter a new password";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    if (password != confirmPassword.Text)
                    {
                        lblMessage.Text = "Passwords do not match";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    cmd = new SqlCommand(updateQuery2, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(password));
                    cmd.Parameters.AddWithValue("@fullName", fullName);
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
                    cmd.Parameters.AddWithValue("@fullName", fullName);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.ExecuteNonQuery();
                }
                lblMessage.Text = "Profile credentials successfully updated";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                Session["Username"] = username;
            }
            finally
            {
                conn.Close();
            }
        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            fullName.Text = string.Empty;
            username.Text = string.Empty;
            password.Text = string.Empty;
            confirmPassword.Text = string.Empty;
        }
    }
}