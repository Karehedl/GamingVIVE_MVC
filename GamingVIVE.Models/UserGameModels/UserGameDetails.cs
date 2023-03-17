using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GameModels;
using GamingVIVE.Models.UsersModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.UserGameModels
{
    public class UserGameDetails
    {
        public int UserGameId { get; set; }
        public GameListItem Game { get; set; }
        public UserListItem User { get; set; }
    }
}
