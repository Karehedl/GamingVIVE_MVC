using Azure.Core;
using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GameModels;
using GamingVIVE.Models.GamingSystemModels;
using GamingVIVE.Models.GSGameModels;
using GamingVIVE_MVC.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.GamingSystemService
{
    public class GamingSystemService : IGamingSystemService
    {

        private readonly ApplicationDbContext _context;
        public GamingSystemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GamingSystemListItem> CreateGamingSystem(GamingSystemCreate gamingSystemCreate)
        {

            var gsdetails = new GamingSystemListItem();
            var gs = new GamingSystem() 
            {
                Name = gamingSystemCreate.Name
            };
            
            await _context.GamingSystems.AddAsync(gs);
            await _context.SaveChangesAsync();

            gsdetails.Name = gs.Name;
            gsdetails.GamingSystemId= gs.GamingSystemId;

            return gsdetails;
        }

        public async Task<bool> DeleteGamingSystem(int gamingSystemId)
        {
            var gs= await _context.GamingSystems.FindAsync(gamingSystemId);


            if (gs == null)
                return false;


            _context.GamingSystems.Remove(gs);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> EditGamingSystem(GamingSystemEdit gamingSystemEdit)
        {
            var gs = await _context.GamingSystems.FindAsync(gamingSystemEdit.GamingSystemId);

            if (gs == null)
                return false;

            gs.Name = gamingSystemEdit.Name;
 
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<List<GamingSystemListItem>> GetAllGamingSystems()
        {
            return await _context.GamingSystems.Select(entity => new GamingSystemListItem
            {
               GamingSystemId= entity.GamingSystemId,
               Name= entity.Name,
            }).ToListAsync();
        }


        public async Task<GamingSystemDetails> GetGamingSystemByName(string search)
        {
            var gs = await _context.GamingSystems.Include(c => c.ConsoleGames).ThenInclude(g => g.Game).FirstOrDefaultAsync(i => i.Name == search);
            if (gs is null)
            {
                return null;
            }

            return new GamingSystemDetails
            {
                GamingSystemId = gs.GamingSystemId,
                Name = gs.Name,
                Games = gs.ConsoleGames.Select(g => g.Game).Select(gl => new Game
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
                //Games = gs.ConsoleGames.Select(ot => ot.Game).ToList()
            };
        }

          public async Task<GamingSystemDetails> GetGamingSystemById(int gamingSystemId)
        {
            var gs = await _context.GamingSystems.Include(c=> c.ConsoleGames).ThenInclude(g=>g.Game).FirstOrDefaultAsync(i => i.GamingSystemId == gamingSystemId);
            if (gs is null)
            {
                return null;
            }

            return new GamingSystemDetails
            {
                GamingSystemId = gs.GamingSystemId,
                Name = gs.Name,
                Games = gs.ConsoleGames.Select(g => g.Game).Select(gl => new Game
                {
                    Name = gl.Name,
                    GameId = gl.GameId,
                    Rating = gl.Rating,
                    Description= gl.Description,
                    Picture= gl.Picture,
                    Publisher= gl.Publisher,
                    Price= gl.Price,
                    ReleaseDate= gl.ReleaseDate,
                }).ToList()
            };
        }
    }

   
             
}
       
