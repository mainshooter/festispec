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
    
    public partial class Quotation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quotation()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public int BtwPercentage { get; set; }
        public Nullable<System.DateTime> TimeSend { get; set; }
        public string Status { get; set; }
        public string Content { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Event Event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        public virtual QuotationStatu QuotationStatu { get; set; }
    }
}
