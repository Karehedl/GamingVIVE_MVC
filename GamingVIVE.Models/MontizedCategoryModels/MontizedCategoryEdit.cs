using GamingVIVE.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.MontizedCategoryModels
{
    public class MontizedCategoryEdit
    {
        [Required]
        public int MontizedCategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Game> CategorizedGames { get; set; } = new List<Game>();
    }
}
