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
    
    public partial class Master_VenueScreen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Master_VenueScreen()
        {
            this.Master_RoleScreen = new HashSet<Master_RoleScreen>();
        }
    
        public int PK_VenueScreenId { get; set; }
        public int FK_VenueId { get; set; }
        public int FK_ScreenId { get; set; }
        public string Prefix { get; set; }
        public int CurrentNo { get; set; }
        public int FK_FormatId { get; set; }
        public int FK_StatusId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual Configuration_Format Configuration_Format { get; set; }
        public virtual Configuration_Screen Configuration_Screen { get; set; }
        public virtual Configuration_Status Configuration_Status { get; set; }
        public virtual Configuration_User Configuration_User { get; set; }
        public virtual Configuration_User Configuration_User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_RoleScreen> Master_RoleScreen { get; set; }
        public virtual Master_Venue Master_Venue { get; set; }
    }
}
