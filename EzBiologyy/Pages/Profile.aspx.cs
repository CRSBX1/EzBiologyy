using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EzBiology.Pages
{
    public partial class Profile : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private SqlCommand cmd, cmd1, cmd2, cmd3;
        private string getQuery = "SELECT * FROM Users WHERE Username=@username";
        private string updateQuery = "UPDATE Users SET Username=@username,Age=@age,Gender=@gender WHERE Username = @oldname";
        private string updateQuery2 = "UPDATE Users SET Username=@username,Password=@password,Age=@age,Gender=@gender WHERE Username = @oldname";
        private string OldUsername
        {
            get => (string)(ViewState["OldUsername"] ?? string.Empty);
            set => ViewState["OldUsername"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack) LoadProfile();
        }

        private void LoadProfile()
        {
            conn.Open();
            cmd = new SqlCommand(getQuery, conn);
            cmd.Parameters.AddWithValue("@username", Session["Username"].ToString());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            OldUsername = dt.Rows[0][1].ToString();
            txtUsername.Text = OldUsername;
            txtAge.Text = dt.Rows[0][4].ToString();
            ddlGender.Text = dt.Rows[0][5].ToString();
            conn.Close();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string age      = txtAge.Text;
            string gender   = ddlGender.SelectedValue;

            try
            {
                conn.Open();
                cmd1 = new SqlCommand(getQuery, conn);
                cmd1.Parameters.AddWithValue("@username", username);
                object result = cmd1.ExecuteScalar();

                if (result != null && username != Session["Username"].ToString())
                {
                    lblMessage.Text = "This username already exists, pick another username";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    conn.Close();
                    return;
                }

                if (ddlPwd.SelectedValue == "Yes")
                {
                    cmd2 = new SqlCommand(updateQuery2, conn);
                    cmd2.Parameters.AddWithValue("@oldname", OldUsername);
                    cmd2.Parameters.AddWithValue("@username", username);
                    cmd2.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(password));
                    cmd2.Parameters.AddWithValue("@age", age);
                    cmd2.Parameters.AddWithValue("@gender", gender);
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    cmd3 = new SqlCommand(updateQuery, conn);
                    cmd3.Parameters.AddWithValue("@oldname", OldUsername);
                    cmd3.Parameters.AddWithValue("@username", username);
                    cmd3.Parameters.AddWithValue("@age", age);
                    cmd3.Parameters.AddWithValue("@gender", gender);
                    cmd3.ExecuteNonQuery();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LoadProfile();
            lblMessage.Text = "";
        }
    }
}
