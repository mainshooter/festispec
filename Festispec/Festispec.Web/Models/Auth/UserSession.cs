using System.Web;
using Festispec.Domain;

namespace Festispec.Web.Models.Auth
{
    public class UserSession
    {
        public static UserSession Current
        {
            get
            {
                if (HttpContext.Current.Session["session"] is UserSession current) return current;

                current = new UserSession();
                HttpContext.Current.Session["session"] = current;

                return current;
            }
        }

        public Employee Employee { get; set; }

        public bool LoggedIn { get; private set; } = true;

        public void Clear()
        {
            Employee = null;
            LoggedIn = false;
        }
    }
}