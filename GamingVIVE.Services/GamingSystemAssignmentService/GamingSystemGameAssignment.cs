using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GameModels;
using GamingVIVE.Models.GamingSystemModels;
using GamingVIVE.Models.GSGameModels;
using GamingVIVE_MVC.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.GamingSystemAssignmentService
{
    public class GamingSystemGameAssignment : IGamingSystemGameAssignment
    {
        private readonly ApplicationDbContext _context;

        public GamingSystemGameAssignment(ApplicationDbContext context)
        {
            _context = context;
        } 

        public async Task<bool> AssignGameToSystem(GamingSystemGameCreate model)
        {
            Game game = await _context.Games.FindAsync(model.GameId);
            if (game == null)
            {
                return false;
            }
            GamingSystem gamingSystem = await _context.GamingSystems.FindAsync(model.GamingSystemId);
            if (gamingSystem == null) {
                return false;
            }


            GamingSystemGame gamingSystemGame = new GamingSystemGame
            {
                GameId = game.GameId,
                GamingSystemId = gamingSystem.GamingSystemId,
            }; 

            await _context.GamingSystemGames.AddAsync(gamingSystemGame);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<GamingSystemGameListItem>> GetAllSystemConnections()
        {
            return await _context.GamingSystemGames.Select(entity => new GamingSystemGameListItem
            {
                Id = entity.Id,
                GameId = entity.GameId,
                GamingSystemId = entity.GamingSystemId,
            }).ToListAsync();
        }

        public async Task<GamingSystemGameDetails> GetSystemConnectionById(int Id)
        {
            var gs = await _context.GamingSystemGames.FirstOrDefaultAsync(i => i.Id == Id);
            if (gs is null)
            {
                return null;
            }

            return new GamingSystemGameDetails
            {
                Id = gs.Id,
                GameId= gs.GameId,
                GamingSystemId = gs.GamingSystemId,
            };
        }

        public async Task<bool> DeleteSystemConnection(int Id)
        {
            var gs = await _context.GamingSystemGames.FindAsync(Id);


            if (gs == null)
                return false;


            _context.GamingSystemGames.Remove(gs);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> EditSystemConnection(GamingSystemGameEdit gamingSystemGameEdit)
        {
            var gs = await _context.GamingSystemGames.FindAsync(gamingSystemGameEdit.Id);

            if (gs == null)
                return false;

            gs.GamingSystemId = gamingSystemGameEdit.GamingSystemId;
            gs.GameId = gamingSystemGameEdit.GameId;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}
