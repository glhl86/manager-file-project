using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("States")]
    public class States
    {
        [Column("Id", TypeName = "bigint")]
        [Key]
        public long Id { get; set; }

        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}
