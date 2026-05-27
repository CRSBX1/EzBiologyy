using System;
using System.Web;

namespace EzBiologyy
{
    public class UserSession
    {
        public static int StudentID
        {
            get
            {
                return Convert.ToInt32(
                HttpContext.Current.Session["UserID"]);
            }
        }

        public static string Username
        {
            get
            {
                return HttpContext.Current.Session["Username"]
                .ToString();
            }
        }
    }
}