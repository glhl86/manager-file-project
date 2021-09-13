using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FileManger.Identity.Models
{
    public class UsersRoles : IdentityUserRole<string>
    {
        public virtual Users Users { get; set; }
        public virtual Role Role { get; set; }
    }
}
