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
    
    public partial class Scheduler
    {
        public int SchedulerID { get; set; }
        public string SchedulerName { get; set; }
        public System.DateTime LastRun { get; set; }
        public string SchedulerProc { get; set; }
        public bool IsNotificationSent { get; set; }
    }
}
