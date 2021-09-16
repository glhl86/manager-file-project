using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CrossCutting.ApiModel
{
    public class StructureAM
    {
        public long StructureId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        public string StructureName { get; set; }
        public string PathFile { get; set; }
        public Stream StreamFile { get; set; }
        public bool IsFile { get; set; }
        public long? FatherStructureId { get; set; }
        public DateTime DateRecord { get; set; }

        public List<StructureAM> ListStrure { get; set; }
    }
}
