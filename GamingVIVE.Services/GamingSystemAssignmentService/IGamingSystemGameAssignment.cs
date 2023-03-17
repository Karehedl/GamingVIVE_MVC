using GamingVIVE.Models.GamingSystemModels;
using GamingVIVE.Models.GSGameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.GamingSystemAssignmentService
{
    public interface IGamingSystemGameAssignment
    {
        Task<bool> AssignGameToSystem(GamingSystemGameCreate model);
        Task<List<GamingSystemGameListItem>> GetAllSystemConnections();
        Task<bool> EditSystemConnection(GamingSystemGameEdit gamingSystemGameEdit);
        Task<bool> DeleteSystemConnection(int Id);
        Task<GamingSystemGameDetails> GetSystemConnectionById(int Id);
    }
}
