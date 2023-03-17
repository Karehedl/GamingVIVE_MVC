using GamingVIVE.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.UserGameModels
{
    internal class UserGameCreate
    {
        [Required]
        public int GameId { get; set; }
        [Required] 
        public string UserId { get; set; }
    }
}
