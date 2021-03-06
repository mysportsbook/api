﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;

    public partial class MySportsBookEntities : DbContext
    {
        public MySportsBookEntities()
            : base("name=MySportsBookEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BatchCount> BatchCounts { get; set; }
        public virtual DbSet<Configuration_BatchType> Configuration_BatchType { get; set; }
        public virtual DbSet<Configuration_Format> Configuration_Format { get; set; }
        public virtual DbSet<Configuration_InvoicePeriod> Configuration_InvoicePeriod { get; set; }
        public virtual DbSet<Configuration_PlayerType> Configuration_PlayerType { get; set; }
        public virtual DbSet<Configuration_Screen> Configuration_Screen { get; set; }
        public virtual DbSet<Configuration_Status> Configuration_Status { get; set; }
        public virtual DbSet<Configuration_User> Configuration_User { get; set; }
        public virtual DbSet<Confirguration_PaymentMode> Confirguration_PaymentMode { get; set; }
        public virtual DbSet<IncomeDetail> IncomeDetails { get; set; }
        public virtual DbSet<Master_Batch> Master_Batch { get; set; }
        public virtual DbSet<Master_BatchTiming> Master_BatchTiming { get; set; }
        public virtual DbSet<Master_CoachingLevel> Master_CoachingLevel { get; set; }
        public virtual DbSet<Master_Court> Master_Court { get; set; }
        public virtual DbSet<Master_Enquiry> Master_Enquiry { get; set; }
        public virtual DbSet<Master_Player> Master_Player { get; set; }
        public virtual DbSet<Master_Role> Master_Role { get; set; }
        public virtual DbSet<Master_RoleScreen> Master_RoleScreen { get; set; }
        public virtual DbSet<Master_ScreenNumberFormat> Master_ScreenNumberFormat { get; set; }
        public virtual DbSet<Master_SMS_Config> Master_SMS_Config { get; set; }
        public virtual DbSet<Master_Sport> Master_Sport { get; set; }
        public virtual DbSet<Master_UserRole> Master_UserRole { get; set; }
        public virtual DbSet<Master_UserVenue> Master_UserVenue { get; set; }
        public virtual DbSet<Master_Venue> Master_Venue { get; set; }
        public virtual DbSet<Master_VenueScreen> Master_VenueScreen { get; set; }
        public virtual DbSet<OtherBooking> OtherBookings { get; set; }
        public virtual DbSet<OtherBookingDetail> OtherBookingDetails { get; set; }
        public virtual DbSet<Studio_ExpenseDetail> Studio_ExpenseDetail { get; set; }
        public virtual DbSet<StudioEvent> StudioEvents { get; set; }
        public virtual DbSet<Transaction_Attendance> Transaction_Attendance { get; set; }
        public virtual DbSet<Transaction_Enquiry_Comments> Transaction_Enquiry_Comments { get; set; }
        public virtual DbSet<Transaction_Invoice> Transaction_Invoice { get; set; }
        public virtual DbSet<Transaction_InvoiceDetail> Transaction_InvoiceDetail { get; set; }
        public virtual DbSet<Transaction_PlayerSport> Transaction_PlayerSport { get; set; }
        public virtual DbSet<Transaction_Receipt> Transaction_Receipt { get; set; }
        public virtual DbSet<Transaction_Voucher> Transaction_Voucher { get; set; }
        public virtual DbSet<Scheduler> Schedulers { get; set; }
        public virtual DbSet<SMSTransactionRequest> SMSTransactionRequests { get; set; }
        public virtual DbSet<V_INVOICEandRECEIPT> V_INVOICEandRECEIPT { get; set; }
        public virtual DbSet<V_InvoiceDetails> V_InvoiceDetails { get; set; }
        public virtual DbSet<V_invoiceWithoutReceipt> V_invoiceWithoutReceipt { get; set; }
        public virtual DbSet<V_PlayerDetails> V_PlayerDetails { get; set; }
        public virtual DbSet<V_PlayerSport> V_PlayerSport { get; set; }
    
        [DbFunction("Entities", "Split")]
        public virtual IQueryable<Split_Result> Split(string inputString, string delimiter)
        {
            var inputStringParameter = inputString != null ?
                new ObjectParameter("InputString", inputString) :
                new ObjectParameter("InputString", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("Delimiter", delimiter) :
                new ObjectParameter("Delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Split_Result>("[Entities].[Split](@InputString, @Delimiter)", inputStringParameter, delimiterParameter);
        }
    
        public virtual ObjectResult<string> GenerateRunningNumber(Nullable<int> venueId, Nullable<int> screenId)
        {
            var venueIdParameter = venueId.HasValue ?
                new ObjectParameter("VenueId", venueId) :
                new ObjectParameter("VenueId", typeof(int));
    
            var screenIdParameter = screenId.HasValue ?
                new ObjectParameter("ScreenId", screenId) :
                new ObjectParameter("ScreenId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GenerateRunningNumber", venueIdParameter, screenIdParameter);
        }
    
        public virtual ObjectResult<rp_COLLECTIONDETAIL_Result> rp_COLLECTIONDETAIL(Nullable<int> vENUEID, Nullable<System.DateTime> mONTH, string tYPE)
        {
            var vENUEIDParameter = vENUEID.HasValue ?
                new ObjectParameter("VENUEID", vENUEID) :
                new ObjectParameter("VENUEID", typeof(int));
    
            var mONTHParameter = mONTH.HasValue ?
                new ObjectParameter("MONTH", mONTH) :
                new ObjectParameter("MONTH", typeof(System.DateTime));
    
            var tYPEParameter = tYPE != null ?
                new ObjectParameter("TYPE", tYPE) :
                new ObjectParameter("TYPE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rp_COLLECTIONDETAIL_Result>("rp_COLLECTIONDETAIL", vENUEIDParameter, mONTHParameter, tYPEParameter);
        }
    
        public virtual ObjectResult<rp_COMMONPROCEDURE_Result> rp_COMMONPROCEDURE(string sTOREPROC, string pARAMETERS, Nullable<int> venueID)
        {
            var sTOREPROCParameter = sTOREPROC != null ?
                new ObjectParameter("STOREPROC", sTOREPROC) :
                new ObjectParameter("STOREPROC", typeof(string));
    
            var pARAMETERSParameter = pARAMETERS != null ?
                new ObjectParameter("PARAMETERS", pARAMETERS) :
                new ObjectParameter("PARAMETERS", typeof(string));
    
            var venueIDParameter = venueID.HasValue ?
                new ObjectParameter("VenueID", venueID) :
                new ObjectParameter("VenueID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rp_COMMONPROCEDURE_Result>("rp_COMMONPROCEDURE", sTOREPROCParameter, pARAMETERSParameter, venueIDParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<Transaction_SaveInvoice_Result> Transaction_SaveInvoice(string xML)
        {
            var xMLParameter = xML != null ?
                new ObjectParameter("XML", xML) :
                new ObjectParameter("XML", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Transaction_SaveInvoice_Result>("Transaction_SaveInvoice", xMLParameter);
        }
    
        public virtual ObjectResult<GetAllUserWithVenues_Result> GetAllUserWithVenues()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllUserWithVenues_Result>("GetAllUserWithVenues");
        }
    
        public virtual int UpdateSchedulerBySchedulerName(string schedulerName)
        {
            var schedulerNameParameter = schedulerName != null ?
                new ObjectParameter("SchedulerName", schedulerName) :
                new ObjectParameter("SchedulerName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateSchedulerBySchedulerName", schedulerNameParameter);
        }
    }
}
