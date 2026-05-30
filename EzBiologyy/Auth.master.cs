using System;
using System.Web.UI;

namespace EzBiology
{
    public partial class AuthMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentPage = System.IO.Path.GetFileNameWithoutExtension(Request.FilePath);

            // On the Login page → right link says "Sign Up" and points to Signup
            // On the Signup page → right link says "Login" and points to Login
            // On any other auth page → falls back to "Home"
            switch (currentPage.ToLower())
            {
                case "login":
                    navRight.InnerText = "Sign Up";
                    navRight.HRef = ResolveUrl("~/Pages/Signup.aspx");
                    break;
                case "signup":
                    navRight.InnerText = "Login";
                    navRight.HRef = ResolveUrl("~/Pages/Login.aspx");
                    break;
            }
        }
    }
}
