using System.Windows;
using Festispec.ViewModel.employee;

namespace Festispec.ViewModel.auth
{
    class UserSessionVM
    {
        public static UserSessionVM Current { 
            get
            {
                if (Application.Current.Resources["session"] is UserSessionVM current) return current;

                current = new UserSessionVM();
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
