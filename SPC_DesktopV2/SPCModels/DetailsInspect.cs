using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_DesktopV2.SPCModels
{
    public class DetailsInspect
    {
        public long ID { get; set; }
        public string No { get; set; }
        public string CheckItem { get; set; }
        public string LSL { get; set; }
        public string Target { get; set; }
        public string USL { get; set; }
        public string MIN { get; set; }
        public string MAX { get; set; }
        public int Multipoint { get; set; }
        public string Formula { get; set; }
        public int SamplingSize { get; set; }
        public string Method { get; set; }
        public string Judgement { get; set; }
        public string Datatype { get; set; }
    }
}
