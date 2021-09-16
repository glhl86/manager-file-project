using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Models
{
    [Table("Structure")]
    public class Structure
    {
        [Column("StructureId", TypeName = "bigint")]
        [Key]
        public long StructureId { get; set; }

        [Column("StructureName", TypeName = "varchar(500)")]
        public string StructureName { get; set; }

        [Column("PathFile", TypeName = "varchar")]
        public string PathFile { get; set; }

        [Column("IsFile", TypeName = "bit")]
        public bool IsFile { get; set; }

        [Column("FatherStructureId", TypeName = "bigint")]
        public long? FatherStructureId { get; set; }

        public DateTime DateRecord { get; set; }

    }
}
