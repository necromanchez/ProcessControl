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
    
    public partial class mSampling
    {
        public long ID { get; set; }
        public int LotSizeLower { get; set; }
        public int LotSizeUpper { get; set; }
        public int Dimension { get; set; }
        public int Visual { get; set; }
        public string UOM { get; set; }
        public string Level { get; set; }
        public string Sample { get; set; }
        public int SampleSize { get; set; }
        public int AccmSampleSize { get; set; }
        public int QANC { get; set; }
        public int QAAC { get; set; }
        public bool IsBoth { get; set; }
        public bool IsDeleted { get; set; }
        public string RegisterID { get; set; }
        public string RegisterPG { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdatePG { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
