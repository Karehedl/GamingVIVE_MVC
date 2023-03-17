using GamingVIVE.Data.Entities;
using GamingVIVE.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.GameModels
{
    public class GameDetails
    {
        public int GameId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Rating Rating { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Publisher { get; set; }
        [Required]
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        public List<GamingSystem> Consoles { get; set; } = new List<GamingSystem>();

        public List<Genre> Genres { get; set; } = new List<Genre>();

        public List<MontizedCategory> Categories { get; set; } = new List<MontizedCategory>();
    }
}
