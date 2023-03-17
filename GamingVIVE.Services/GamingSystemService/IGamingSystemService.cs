using GamingVIVE.Models.GameModels;
using GamingVIVE.Models.GamingSystemModels;
using GamingVIVE.Models.GSGameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.GamingSystemService
{
    public interface IGamingSystemService
    {

        public Task<GamingSystemListItem> CreateGamingSystem(GamingSystemCreate gamingSystemCreate);

        public Task<List<GamingSystemListItem>> GetAllGamingSystems();

        public Task<bool> DeleteGamingSystem(int gamingSystemId);

        public Task<bool> EditGamingSystem(GamingSystemEdit gamingSystemEdit);

        public Task<GamingSystemDetails> GetGamingSystemByName(string search);

        public Task<GamingSystemDetails> GetGamingSystemById(int gamingSystemId);
    }
}
