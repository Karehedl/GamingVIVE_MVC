using GamingVIVE.Data.Entities;
using GamingVIVE.Data.Entities.Enums;
using GamingVIVE.Models.GameModels;
using GamingVIVE.Models.GamingSystemModels;
using GamingVIVE.Services.GamesServices;
using GamingVIVE_MVC.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> GameCreateAsync(GameCreate model)
        {
            var game = new Game()
            {
                Name = model.Name,
                Rating = model.Rating,
                Description = model.Description,
                Picture = model.Picture,
                Publisher = model.Publisher,
                Price = model.Price,
                ReleaseDate = DateTime.Now,
            };

            await _context.Games.AddAsync(game);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }


        public async Task<List<GameListItem>> GetAllGames()
        {
            return await _context.Games
            .Select(g => new GameListItem
            {
                GameId= g.GameId,
                Picture= g.Picture,
                Name = g.Name,
                Price = g.Price,
            }).ToListAsync();
        }


        public async Task<bool> EditGame(GameEdit gameEdit)
        {
            var game = await _context.Games.FindAsync(gameEdit.GameId);

            if (game == null)
                return false;

            game.Name = gameEdit.Name;
            game.Description = gameEdit.Description;
            game.Price = gameEdit.Price;
            game.GameId = gameEdit.GameId;
            game.Rating = gameEdit.Rating;
            game.Picture = gameEdit.Picture;
            game.Publisher = gameEdit.Publisher;
            game.ReleaseDate = gameEdit.ReleaseDate;
            

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteGame(int gameId)
        {
            
            var game = await _context.Games.FindAsync(gameId);

            
            if (game == null)
                return false;

           
            _context.Games.Remove(game);
            return await _context.SaveChangesAsync() == 1;

        }

        public async Task<GameDetails> GetGameById(int gameId)
        {

            var game = await _context.Games
                .Include(c => c.FeaturedConsoles)
                    .ThenInclude(g => g.GamingSystem)
                .Include(c => c.GameGenres)
                    .ThenInclude(g => g.Genre)
                .Include(c => c.MontizedCategories)
                    .ThenInclude(g => g.MontizedCategory)
                .FirstOrDefaultAsync(i => i.GameId == gameId);
            if (game is null)
            {
                return null;
            }

            return new GameDetails
            {
                GameId = game.GameId,
                Name = game.Name,
                Rating = game.Rating,
                Description = game.Description,
                Picture = game.Picture,
                Publisher = game.Publisher,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                Consoles = game.FeaturedConsoles.Select(gs => gs.GamingSystem).Select(gsl => new GamingSystem
                {
                    GamingSystemId = gsl.GamingSystemId,
                    Name = gsl.Name,
                }).ToList(),
                Genres = game.GameGenres.Select(g => g.Genre).Select(gl => new Genre
                 {
                     GenreId = gl.GenreId,
                     Name = gl.Name,
                 }).ToList(),
                Categories = game.MontizedCategories.Select(g => g.MontizedCategory).Select(gl => new MontizedCategory
                {
                    MontizedCategoryId = gl.MontizedCategoryId,
                    Name = gl.Name,
                }).ToList(),

            };
        }

        public async Task<GameDetails> GetGameByName(string search)
        {
            var games = await _context.Games
                .Include(c => c.FeaturedConsoles)
                    .ThenInclude(g => g.GamingSystem)
                .Include(c => c.GameGenres)
                    .ThenInclude(g => g.Genre)
                .Include(c => c.MontizedCategories)
                    .ThenInclude(g => g.MontizedCategory).FirstOrDefaultAsync(i => i.Name == search);
            if (games is null)
            {
                return null;
            }

            return new GameDetails
            {
                GameId = games.GameId,
                Name = games.Name,
                Rating = games.Rating,
                Description = games.Description,
                Picture = games.Picture,
                Publisher = games.Publisher,
                Price = games.Price,
                ReleaseDate = games.ReleaseDate,
                Consoles = games.FeaturedConsoles.Select(gs => gs.GamingSystem).Select(gsl => new GamingSystem
                {
                    GamingSystemId = gsl.GamingSystemId,
                    Name = gsl.Name,
                }).ToList(),
                Genres = games.GameGenres.Select(g => g.Genre).Select(gl => new Genre
                {
                    GenreId = gl.GenreId,
                    Name = gl.Name,
                }).ToList(),
                Categories = games.MontizedCategories.Select(g => g.MontizedCategory).Select(gl => new MontizedCategory
                {
                    MontizedCategoryId = gl.MontizedCategoryId,
                    Name = gl.Name,
                }).ToList(),

            };
        }
    }
}
