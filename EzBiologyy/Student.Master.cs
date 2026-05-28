using System;

namespace EzBiologyy
{
    public partial class Student : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void NavToDashboard(object sender, EventArgs e)
        {
            Response.Redirect("StudentDashboard.aspx");
        }

        protected void NavToCourses(object sender, EventArgs e)
        {
            Response.Redirect("StudentCourses.aspx");
        }

        protected void NavToGrades(object sender, EventArgs e)
        {
            Response.Redirect("StudentGrades.aspx");
        }

        protected void NavToAssessments(object sender, EventArgs e)
        {
            Response.Redirect("StudentAssessmentMenu.aspx");
        }

        protected void NavToForums(object sender, EventArgs e)
        {
            Response.Redirect("StudentForum.aspx");
        }

        protected void NavToLogout(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}