//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DrinkManager
{
    using System;
    using System.Collections.Generic;
    
    public partial class BILL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BILL()
        {
            this.DETAIL_BILLS = new HashSet<DETAIL_BILLS>();
        }
    
        public int bid { get; set; }
        public Nullable<int> cid { get; set; }
        public Nullable<long> price { get; set; }
        public Nullable<int> cashier { get; set; }
        public Nullable<System.DateTime> purchasedate { get; set; }
        public Nullable<int> tid { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETAIL_BILLS> DETAIL_BILLS { get; set; }
        public virtual CUSTOMER CUSTOMER { get; set; }
        public virtual EMPLOYEE EMPLOYEE { get; set; }
        public virtual TABLE TABLE { get; set; }
    }
}
