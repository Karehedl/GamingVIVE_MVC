using GamingVIVE.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Models.UserGameModels
{
    public class UserGameListItem
    {
        //Purchase
        public string UserId { get; set; } = null!;
        public int GameId { get; set; } 
            
       
    }
}
