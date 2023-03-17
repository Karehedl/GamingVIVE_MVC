using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GenreGameModels;
using GamingVIVE_MVC.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.GenreAssignmentService
{
    public class GenreGameAssignment : IGenreGameAssignment
    {
        private readonly ApplicationDbContext _context;

        public GenreGameAssignment(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AssignGameToGenres(GenreGameCreate model)
        {
            Game game = await _context.Games.FindAsync(model.GameId);
            if (game == null)
            {
                return false;
            }
            Genre genre = await _context.Genres.FindAsync(model.GenreId);
            if (genre == null)
            {
                return false;
            }


            GenreGame genreGame = new GenreGame
            {
                GameId = game.GameId,
                GenreId = genre.GenreId,
            };

            await _context.GenreGames.AddAsync(genreGame);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<GenreGameListItem>> GetAllGenreConnections()
        {
            return await _context.GenreGames.Select(entity => new GenreGameListItem
            {
                Id = entity.Id,
                GameId = entity.GameId,
                GenreId = entity.GenreId,
            }).ToListAsync();
        }

        public async Task<GenreGameDetails> GetGenreConnectionById(int Id)
        {
            var genre = await _context.GenreGames.FirstOrDefaultAsync(i => i.Id == Id);
            if (genre is null)
            {
                return null;
            }

            return new GenreGameDetails
            {
                Id = genre.Id,
                GameId = genre.GameId,
                GenreId = genre.GenreId,
            };
        }

        public async Task<bool> DeleteGenreConnection(int Id)
        {
            var genre = await _context.GenreGames.FindAsync(Id);


            if (genre == null)
                return false;


            _context.GenreGames.Remove(genre);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> EditGenreConnection(GenreGameEdit genreGameEdit)
        {
            var genre = await _context.GenreGames.FindAsync(genreGameEdit.Id);

            if (genre == null)
                return false;

            genre.GenreId = genreGameEdit.GenreId;
            genre.GameId = genreGameEdit.GameId;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}
