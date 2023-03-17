using GamingVIVE.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Data.Entities
{
    public class Game
    {
        [Key]
        [Required]
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

        public List<GamingSystemGame> FeaturedConsoles { get; set; } = new List<GamingSystemGame>();

        public List<GenreGame> GameGenres { get; set; } = new List<GenreGame>();

        public List<MontizedCategoryGame> MontizedCategories { get; set; } = new List<MontizedCategoryGame>();
    }
}
