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
    
    public partial class Master_UserVenue
    {
        public int PK_UserVenueId { get; set; }
        public int FK_UserId { get; set; }
        public int FK_VenueId { get; set; }
        public int FK_StatusId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool DailyCollectionEmail { get; set; }
        public bool DailyCollectionSMS { get; set; }
    
        public virtual Configuration_Status Configuration_Status { get; set; }
        public virtual Configuration_User Configuration_User { get; set; }
        public virtual Configuration_User Configuration_User1 { get; set; }
        public virtual Configuration_User Configuration_User2 { get; set; }
        public virtual Master_Venue Master_Venue { get; set; }
    }
}
