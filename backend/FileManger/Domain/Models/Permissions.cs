using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Permissions")]
    public class Permissions
    {
        [Column("PermissionsId", TypeName = "bigint")]
        [Key]
        public long PermissionsId { get; set; }

        [Column("StructureId", TypeName = "bigint")]
        public long StructureId { get; set; }

        [Column("UserId", TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

        [ForeignKey("StructureId")]
        public virtual Structure Structure { get; set; }
    }
}
