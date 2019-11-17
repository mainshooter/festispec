using System.Windows;
using Festispec.Domain;
using Festispec.ViewModel.employee;

namespace Festispec.ViewModel.auth
{
    class UserSession
    {
        public static UserSession Current { 
            get
            {
                if (Application.Current.Resources["session"] is UserSession current) return current;

                current = new UserSession();
                Application.Current.Resources["session"] = current;

                return current;
            }
        }

        public EmployeeVM Employee { get; set; }

        public bool LoggedIn { get; private set; } = true;

        public void Clear()
        {
            Employee = null;
            LoggedIn = false;
        }
    }
}
