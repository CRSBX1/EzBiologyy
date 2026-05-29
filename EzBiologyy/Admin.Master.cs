using System;

namespace EzBiologyy
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string page = System.IO.Path.GetFileNameWithoutExtension(Request.Path).ToLower();
            switch (page)
            {
                case "admindashboard":
                    navHomeAdmin.Attributes["class"] = "active";
                    break;
                case "adminmanage":
                case "admincreate":
                case "adminedit":
                    navUsersAdmin.Attributes["class"] = "active";
                    break;
                case "adminprofile":
                    navProfileAdmin.Attributes["class"] = "active";
                    break;
            }
        }

        protected void NavToLogout(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Pages/Login.aspx");
        }
    }
}
