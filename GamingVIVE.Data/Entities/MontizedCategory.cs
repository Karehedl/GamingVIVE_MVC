using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Data.Entities
{
    public class MontizedCategory
    {
        [Key]
        public int MontizedCategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public List<MontizedCategoryGame> MontizedCategories { get; set; } = new List<MontizedCategoryGame>();
    }
}
