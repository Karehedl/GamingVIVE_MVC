using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GamingSystemModels;
using GamingVIVE.Models.MontizedCategoryModels;
using GamingVIVE_MVC.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.MontizedCategoryService
{
    public class MontizedCategoryService : IMontizedCategoryService
    {
        private readonly ApplicationDbContext _context;
        public MontizedCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MontizedCategoryListItem> CreateMontizedCategory(MontizedCategoryCreate montizedCategoryCreate)
        {

            var details = new MontizedCategoryListItem();
            var mc = new MontizedCategory()
            {
                Name = montizedCategoryCreate.Name
            };

            await _context.MontizedCategories.AddAsync(mc);
            await _context.SaveChangesAsync();

            details.Name = mc.Name;
            details.MontizedCategoryId = mc.MontizedCategoryId;

            return details;
        }

        public async Task<bool> DeleteMontizedCategory(int montizedCategoryId)
        {
            var mc = await _context.MontizedCategories.FindAsync(montizedCategoryId);


            if (mc == null)
                return false;


            _context.MontizedCategories.Remove(mc);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> EditMontizedCategory(MontizedCategoryEdit montizedCategoryEdit)
        {
            var mc = await _context.MontizedCategories.FindAsync(montizedCategoryEdit.MontizedCategoryId);

            if (mc == null)
                return false;

            mc.Name = montizedCategoryEdit.Name;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<List<MontizedCategoryListItem>> GetAllMontizedCategories()
        {
            return await _context.MontizedCategories.Select(entity => new MontizedCategoryListItem
            {
                MontizedCategoryId = entity.MontizedCategoryId,
                Name = entity.Name,
            }).ToListAsync();
        }

        public async Task<MontizedCategoryDetails> GetMontizedCategoryByName(string search)
        {
            var mc = await _context.MontizedCategories.Include(c => c.MontizedCategories).ThenInclude(g => g.Game).FirstOrDefaultAsync(i => i.Name == search);
            if (mc is null)
            {
                return null;
            }

            return new MontizedCategoryDetails
            {
                MontizedCategoryId = mc.MontizedCategoryId,
                Name = mc.Name,
                CategorizedGames = mc.MontizedCategories.Select(g => g.Game).Select(gl => new Game
                {
                    Name = gl.Name,
                    GameId = gl.GameId,
                    Rating = gl.Rating,
                    Description = gl.Description,
                    Picture = gl.Picture,
                    Publisher = gl.Publisher,
                    Price = gl.Price,
                    ReleaseDate = gl.ReleaseDate,
                }).ToList()
            };
        }

        public async Task<MontizedCategoryDetails> GetMontizedCategoryById(int montizedCategoryId)
        {
            var mc = await _context.MontizedCategories.Include(c => c.MontizedCategories).ThenInclude(g => g.Game).FirstOrDefaultAsync(i => i.MontizedCategoryId == montizedCategoryId);
            if (mc is null)
            {
                return null;
            }

            return new MontizedCategoryDetails
            {
                MontizedCategoryId = mc.MontizedCategoryId,
                Name = mc.Name,
                CategorizedGames = mc.MontizedCategories.Select(g => g.Game).Select(gl => new Game
                {
                    Name = gl.Name,
                    GameId = gl.GameId,
                    Rating = gl.Rating,
                    Description = gl.Description,
                    Picture = gl.Picture,
                    Publisher = gl.Publisher,
                    Price = gl.Price,
                    ReleaseDate = gl.ReleaseDate,
                }).ToList()
            };
        }
    }
}
