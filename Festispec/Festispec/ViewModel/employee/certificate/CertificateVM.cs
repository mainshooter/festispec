﻿using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.certificate
{
    public class CertificateVM
    {
        public int Id { 
            get {
                return _certificate.Id;
            }
            private set {
                _certificate.Id = value;
            }
        }
        private EmployeeVM _employee;
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

        private CertificateInspector _certificate;
        public CertificateVM(CertificateInspector certificate)
        {
            _certificate = certificate;
            Employee = new EmployeeVM(certificate.Employee);
        }
    }
}
