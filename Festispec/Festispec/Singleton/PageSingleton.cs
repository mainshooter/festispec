using Festispec.View.Pages;
using Festispec.View.Pages.Availability;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Employee;
using Festispec.View.Pages.Event;
using Festispec.View.Pages.Sick;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Festispec.Singleton
{
    class PageSingleton
    {
        private Dictionary<string, Page> _pages;

        public PageSingleton()
        {
            _pages = new Dictionary<string, Page>();
            _pages.Add("dashboard", new DashboardPage());
            _pages.Add("employee", new EmployeePage());
            _pages.Add("customer", new CustomerPage());
            _pages.Add("availability", new AvailablePage());
            _pages.Add("event", new EventPage());
            _pages.Add("sick", new SickPage());
        }

        public Page GetPage(string pageName)
        {
            var result = _pages.Where(p => p.Key.Equals(pageName));
            return result.FirstOrDefault().Value;
        }
    }
}
