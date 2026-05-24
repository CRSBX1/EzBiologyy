using System;

namespace EzBiologyy
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void NavToHome(object sender, EventArgs e)
        {
            Response.Redirect("AdminDashboard.aspx");
        }
        protected void NavToUsers(object sender, EventArgs e)
        {
            Response.Redirect("AdminManage.aspx");
        }

        protected void NavToProfile(object sender, EventArgs e)
        {
            Response.Redirect("AdminProfile.aspx");
        }

        protected void NavToLogout(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}