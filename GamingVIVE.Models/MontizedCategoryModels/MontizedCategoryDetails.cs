using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GameModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.MontizedCategoryModels
{
    public class MontizedCategoryDetails
    {
        
        public int MontizedCategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Game> CategorizedGames { get; set; } = new List<Game>();
    }
}
