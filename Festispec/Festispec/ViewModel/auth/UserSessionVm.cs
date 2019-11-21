using System.Windows;
using Festispec.ViewModel.employee;

namespace Festispec.ViewModel.auth
{
    class UserSessionVm
    {
        public static UserSessionVm Current { 
            get
            {
                if (Application.Current.Resources["session"] is UserSessionVm current) return current;

                current = new UserSessionVm();
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
