//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPC_DesktopV2
{
    using System;
    using System.Collections.Generic;
    
    public partial class mMailRecipient
    {
        public long MRID { get; set; }
        public string MRName { get; set; }
        public string MREmail { get; set; }
        public bool NewMCS { get; set; }
        public bool RevisedMCS { get; set; }
        public bool ApprovedNew { get; set; }
        public bool RejectedNew { get; set; }
        public bool ApprovedRevised { get; set; }
        public bool RejectedRevised { get; set; }
        public bool IsDeleted { get; set; }
        public string RegisterID { get; set; }
        public string RegisterPG { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdatePG { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}