//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MySportsBookModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Configuration_Status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Configuration_Status()
        {
            this.Configuration_User = new HashSet<Configuration_User>();
            this.Master_CoachingLevel = new HashSet<Master_CoachingLevel>();
            this.Master_Court = new HashSet<Master_Court>();
            this.Master_Player = new HashSet<Master_Player>();
            this.Master_Role = new HashSet<Master_Role>();
            this.Master_RoleScreen = new HashSet<Master_RoleScreen>();
            this.Master_ScreenNumberFormat = new HashSet<Master_ScreenNumberFormat>();
            this.Master_Sport = new HashSet<Master_Sport>();
            this.Master_UserVenue = new HashSet<Master_UserVenue>();
            this.Master_Venue = new HashSet<Master_Venue>();
            this.Transaction_Invoice = new HashSet<Transaction_Invoice>();
            this.Transaction_InvoiceDetail = new HashSet<Transaction_InvoiceDetail>();
            this.Transaction_PlayerSport = new HashSet<Transaction_PlayerSport>();
            this.Transaction_Receipt = new HashSet<Transaction_Receipt>();
            this.Transaction_Voucher = new HashSet<Transaction_Voucher>();
            this.Master_Batch = new HashSet<Master_Batch>();
        }
    
        public int PK_StatusId { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configuration_User> Configuration_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_CoachingLevel> Master_CoachingLevel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_Court> Master_Court { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_Player> Master_Player { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_Role> Master_Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_RoleScreen> Master_RoleScreen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_ScreenNumberFormat> Master_ScreenNumberFormat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_Sport> Master_Sport { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_UserVenue> Master_UserVenue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_Venue> Master_Venue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction_Invoice> Transaction_Invoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction_InvoiceDetail> Transaction_InvoiceDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction_PlayerSport> Transaction_PlayerSport { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction_Receipt> Transaction_Receipt { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction_Voucher> Transaction_Voucher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_Batch> Master_Batch { get; set; }
    }
}
