using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Data.Entities
{
    public class GamingSystem
    {
        [Key]
        public int GamingSystemId { get; set; }
        [Required]
        public string Name { get; set; }

        public List<GamingSystemGame> ConsoleGames { get; set; } = new List<GamingSystemGame>();

    }
}
