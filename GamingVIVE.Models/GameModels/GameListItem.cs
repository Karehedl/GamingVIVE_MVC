using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.GameModels
{
    public class GameListItem
    {
        public int GameId { get; set; }
        public string? Picture { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

    }
}
