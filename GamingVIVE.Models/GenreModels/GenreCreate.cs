using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.GenreModels
{
    public class GenreCreate
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Can not exceed 50 characters.")]
        public string Name { get; set; } = null!;
    }
}
