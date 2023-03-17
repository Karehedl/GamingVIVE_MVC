using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Data.Entities
{
    public class MontizedCategoryGame
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public virtual Game Game { get; set; }

        public int MontizedCategoryId { get; set; }
        [ForeignKey(nameof(MontizedCategoryId))]
        public virtual MontizedCategory MontizedCategory { get; set; }
    }
}
