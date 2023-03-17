using GamingVIVE.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.GamingSystemModels
{
    public class GamingSystemEdit
    {
        [Key]
        public int GamingSystemId { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();

    }
}
