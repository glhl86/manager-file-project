using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Person")]
    public class Person
    {
        [Column("Id", TypeName = "bigint")]
        [Key]
        public long Id { get; set; }

        [Column("Name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column("Second_Name", TypeName = "varchar(100)")]
        public string SecondName { get; set; }

        [Column("Surname", TypeName = "varchar(100)")]
        public string Surname { get; set; }

        [Column("Second_Surname", TypeName = "varchar(100)")]
        public string SecondSurname { get; set; }

        [Column("Identification", TypeName = "varchar(20)")]
        public string Identification { get; set; }

        [Column("Cellphone", TypeName = "varchar(50)")]
        public string Cellphone { get; set; }

        [Column("Registration_Date", TypeName = "datetime")]
        public DateTime RegistrationDate { get; set; }

        [Column("Id_State", TypeName = "bigint")]
        public long? IdState { get; set; }

        [ForeignKey("IdState")]
        public virtual States States { get; set; }
    }
}
