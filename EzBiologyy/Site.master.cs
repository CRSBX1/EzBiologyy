using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EzBiology
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session == null) {
                Response.Redirect("Login.aspx");
            }
        }

        protected void NavToHome(object sender, EventArgs e){
            Response.Redirect("Home.aspx");
        }

        protected void NavToCourses(object sender, EventArgs e)
        {
            Response.Redirect("Courses.aspx");
        }

        protected void NavToMaterials(object sender, EventArgs e)
        {
            Response.Redirect("Materials.aspx");
        }

        protected void NavToAssessments(object sender, EventArgs e)
        {
            Response.Redirect("Assessments.aspx");
        }

        protected void NavToReview(object sender, EventArgs e)
        {
            Response.Redirect("ReviewStudents.aspx");
        }

        protected void NavToProfile(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }

        protected void NavToLogout(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}
