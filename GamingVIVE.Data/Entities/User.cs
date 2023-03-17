using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Data.Entities
{
    public class User : IdentityUser
    {
        public DateTime DateCreated { get; set; }
        public string? Picture { get; set; }
        public decimal Balance { get; set; }
    }
}
