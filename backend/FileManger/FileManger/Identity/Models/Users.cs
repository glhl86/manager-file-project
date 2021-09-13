using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace FileManger.Identity.Models
{
    public class Users : IdentityUser
    {
        [Column("Id_Person", TypeName = "bigint")]
        public Nullable<long> IdPerson { get; set; }

        [Column("Id_State", TypeName = "bigint")]
        public long IdState { get; set; }

        [ForeignKey("IdPerson")]
        public virtual Person Person { get; set; }

        public ICollection<UsersRoles> UsersRoles { get; set; }
    }
}
