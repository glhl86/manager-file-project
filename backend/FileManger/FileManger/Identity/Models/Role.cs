using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FileManger.Identity.Models
{
    public class Role: IdentityRole
    {
        public ICollection<UsersRoles> UsersRoles { get; set; }
    }
}
