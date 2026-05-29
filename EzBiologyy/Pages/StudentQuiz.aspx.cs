using System;

namespace EzBiologyy.Pages
{
    public partial class StudentQuiz : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}