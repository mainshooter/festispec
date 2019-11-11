using System;
using Festispec.ViewModel.employee.department;

namespace Festispec.Interface
{
    public interface IEmployee
    {
        int Id { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Street { get; set; }
        int HouseNumber { get; set; }
        string City { get; set; }
        string PostalCode { get; set; }
        string Password { get; set; }
        string PasswordResetToken { get; set; }
        DateTime ResetTokenEndTime { get; set; }
        string Iban { get; set; }
        string Status { get; set; }

        DepartmentVM Department { get; set; }

        bool HasRole(string role);
    }
}
