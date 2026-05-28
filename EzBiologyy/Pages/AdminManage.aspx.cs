using System;

namespace EzBiologyy.Pages
{
    public partial class AdminManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Unnamed7_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminCreate.aspx");
        }
    }
}