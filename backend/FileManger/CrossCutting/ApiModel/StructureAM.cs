using System;

namespace CrossCutting.ApiModel
{
    public class StructureAM
    {
        public long StructureId { get; set; }
        public string StructureName { get; set; }
        public string PathFile { get; set; }
        public bool IsFile { get; set; }
        public long? FatherStructureId { get; set; }
        public DateTime DateRecord { get; set; }
    }
}
