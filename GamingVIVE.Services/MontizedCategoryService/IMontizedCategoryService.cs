using GamingVIVE.Models.MontizedCategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.MontizedCategoryService
{
     public interface IMontizedCategoryService
    {
        public Task<MontizedCategoryListItem> CreateMontizedCategory(MontizedCategoryCreate montizedCategoryCreate);

        public Task<bool> DeleteMontizedCategory(int montizedCategoryId);

        public Task<bool> EditMontizedCategory(MontizedCategoryEdit montizedCategoryEdit);
        public Task<List<MontizedCategoryListItem>> GetAllMontizedCategories();
        public Task<MontizedCategoryDetails> GetMontizedCategoryById(int montizedCategoryId);

        public Task<MontizedCategoryDetails> GetMontizedCategoryByName(string search);
    }
}
