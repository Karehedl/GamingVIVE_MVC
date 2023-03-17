using GamingVIVE.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.GSGameModels
{
    public class GamingSystemGameCreate
    {
        public int GameId { get; set; }
        public int GamingSystemId { get; set; }
    }
}
