using System.Collections.Generic;
using System.Linq;
using Festispec.Domain;
using Festispec.ViewModel.customer;

namespace Festispec.Repository
{
    public class CustomerRepository
    {
        public List<CustomerVM> GetCustomers()
        {
            var result = new List<CustomerVM>();

            using (var context = new Entities())
            {
                result = new List<CustomerVM>(context.Customers.ToList().Select(customer => new CustomerVM(customer)));
            }

            return result;
        }
    }
}
