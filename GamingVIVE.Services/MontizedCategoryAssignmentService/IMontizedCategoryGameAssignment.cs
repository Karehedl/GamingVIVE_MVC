using GamingVIVE.Models.MCGameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.MontizedCategoryAssignmentService
{
    public interface IMontizedCategoryGameAssignment
    {
        Task<bool> EditConnection(MontizedCategoryGameEdit montizedCategoryGameEdit);
        Task<MontizedCategoryGameDetails> GetConnectionById(int Id);
        Task<bool> DeleteConnection(int Id);
        Task<List<MontizedCategoryGameListItem>> GetAllConnections();
        Task<bool> AssignGameToCategory(MontizedCategoryGameCreate model);
    }
}
