﻿using GamingVIVE.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.GameModels
{
    public class GameCreate
    {
        public string Name { get; set; }
        [Required]
        public Rating Rating { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Publisher { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int GamingSystemId { get; set; }
        public int GenreId { get; set; }
    }
}
