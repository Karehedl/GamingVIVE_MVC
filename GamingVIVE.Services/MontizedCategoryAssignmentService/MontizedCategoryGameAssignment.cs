using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GSGameModels;
using GamingVIVE.Models.MCGameModels;
using GamingVIVE_MVC.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.MontizedCategoryAssignmentService
{
    public class MontizedCategoryGameAssignment : IMontizedCategoryGameAssignment
    {
        private readonly ApplicationDbContext _context;

        public MontizedCategoryGameAssignment(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AssignGameToCategory(MontizedCategoryGameCreate model)
        {
            Game game = await _context.Games.FindAsync(model.GameId);
            if (game == null)
            {
                return false;
            }
            MontizedCategory montizedCategory = await _context.MontizedCategories.FindAsync(model.MontizedCategoryId);
            if (montizedCategory == null)
            {
                return false;
            }


            MontizedCategoryGame montizedCategoryGame = new MontizedCategoryGame
            {
                GameId = game.GameId,
                MontizedCategoryId = montizedCategory.MontizedCategoryId,
            };

            await _context.MontizedCategoryGames.AddAsync(montizedCategoryGame);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<MontizedCategoryGameListItem>> GetAllConnections()
        {
            return await _context.MontizedCategoryGames.Select(entity => new MontizedCategoryGameListItem
            {
                Id = entity.Id,
                GameId = entity.GameId,
                MontizedCategoryId = entity.MontizedCategoryId,
            }).ToListAsync();
        }

        public async Task<MontizedCategoryGameDetails> GetConnectionById(int Id)
        {
            var gs = await _context.MontizedCategoryGames.FirstOrDefaultAsync(i => i.Id == Id);
            if (gs is null)
            {
                return null;
            }

            return new MontizedCategoryGameDetails
            {
                Id = gs.Id,
                GameId = gs.GameId,
                MontizedCategoryId = gs.MontizedCategoryId,
            };
        }

        public async Task<bool> DeleteConnection(int Id)
        {
            var gs = await _context.MontizedCategoryGames.FindAsync(Id);


            if (gs == null)
                return false;


            _context.MontizedCategoryGames.Remove(gs);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> EditConnection(MontizedCategoryGameEdit montizedCategoryGameEdit)
        {
            var gs = await _context.MontizedCategoryGames.FindAsync(montizedCategoryGameEdit.Id);

            if (gs == null)
                return false;

            gs.MontizedCategoryId = montizedCategoryGameEdit.MontizedCategoryId;
            gs.GameId = montizedCategoryGameEdit.GameId;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}
