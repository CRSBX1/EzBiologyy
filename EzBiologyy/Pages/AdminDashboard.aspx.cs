using System;

namespace EzBiologyy.Pages
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                greetingLbl.Text = "Hello, Admin " + Session["Username"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Unnamed5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/AdminCreate");
        }
    }
}