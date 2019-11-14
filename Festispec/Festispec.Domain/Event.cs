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
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Orders = new HashSet<Order>();
            this.Quotations = new HashSet<Quotation>();
        }
    
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ContactPersonId { get; set; }
        public string Name { get; set; }
        public System.DateTime BeginDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int AmountVisitors { get; set; }
        public int SurfaceM2 { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string HouseNumber_Addition { get; set; }
    
        public virtual ContactPerson ContactPerson { get; set; }
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
