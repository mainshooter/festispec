//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Festispec.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class SickReportInspector
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int InspectorPlanningEmployeeId { get; set; }
        public int InspoctorPlanningDayId { get; set; }
        public int InspectorPlanningOrderId { get; set; }
        public string Reason { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual InspectorPlanning InspectorPlanning { get; set; }
    }
}
