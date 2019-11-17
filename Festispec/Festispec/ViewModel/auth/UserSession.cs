using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Festispec.Domain;

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

        public Employee Employee { get; set; }

        public bool LoggedIn { get; private set; } = true;

        public void Clear()
        {
            Employee = null;
            LoggedIn = false;
        }
    }
}
