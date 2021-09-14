using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.ApiModel
{
    public class PermissionsAM
    {
        public long PermissionsId { get; set; }
        public long StructureId { get; set; }
        public string UserId { get; set; }
        public virtual StructureAM Structure { get; set; }
    }
}
