using GamingVIVE.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.UsersModels
{
    public class UserDetail
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<GameListItem> GamesLibrary { get; set; } = new List<GameListItem>();
    }
}
