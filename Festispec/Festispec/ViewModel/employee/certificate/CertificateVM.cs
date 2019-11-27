using Festispec.Domain;
using System;

namespace Festispec.ViewModel.employee.certificate
{
    public class CertificateVM
    {
        private EmployeeVM _employee;
        private CertificateInspector _certificate;

        public int Id { 
            get {
                return _certificate.Id;
            }
            private set {
                _certificate.Id = value;
            }
        }
        
        public EmployeeVM Employee {
            get {
                return _employee;
            }
            set {
                _employee = value;
            }
        }

        public DateTime StartDate {
            get {
                return _certificate.DateFrom;
            }
            set {
                _certificate.DateFrom = value;
            }
        }

        public DateTime ValidUntil {
            get {
                return _certificate.DateTill;
            }
            set {
                _certificate.DateTill = value;
            }
        }

        public CertificateVM(CertificateInspector certificate)
        {
            _certificate = certificate;
            Employee = new EmployeeVM(certificate.Employee);
        }

        public CertificateVM()
        {
            _certificate = new CertificateInspector();
        }
    }
}
