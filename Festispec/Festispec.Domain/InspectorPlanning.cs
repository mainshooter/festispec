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
    
    public partial class InspectorPlanning
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InspectorPlanning()
        {
            this.SickReportInspectors = new HashSet<SickReportInspector>();
        }
    
        public int EmployeeId { get; set; }
        public int DayId { get; set; }
        public int OrderId { get; set; }
        public System.DateTime PlannedFrom { get; set; }
        public System.DateTime PlannedTill { get; set; }
        public Nullable<System.DateTime> WorkedFrom { get; set; }
        public Nullable<System.DateTime> WorkedTill { get; set; }
    
        public virtual Day Day { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SickReportInspector> SickReportInspectors { get; set; }
    }
}
