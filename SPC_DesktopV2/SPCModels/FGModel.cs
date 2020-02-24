using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_DesktopV2.SPCModels
{
    public class FGModel
    {
        public string MCSID { get; set; }
        public string MCSFGID { get; set; }
        public string No { get; set; }
        public string CheckItem { get; set; }
        public string Method { get; set; }
        public string Process { get; set; }
        public string NominalValue { get; set; }
        public string CTQMin { get; set; }
        public string CTQMax { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string DataType { get; set; }
        public string Calculation { get; set; }
        public int MPCNo { get; set; }
        public string MPCCriteria { get; set; }
        public string Remarks { get; set; }
    }
}
