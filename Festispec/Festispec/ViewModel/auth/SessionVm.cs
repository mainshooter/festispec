using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Festispec.Domain;

namespace Festispec.ViewModel.auth
{
    public class SessionVm
    {
        public bool LoggedIn { get; set; }
        public Employee User { get; set; }

        public SessionVm(Employee user)
        {
            User = user;
            LoggedIn = true;
        }
    }
}
