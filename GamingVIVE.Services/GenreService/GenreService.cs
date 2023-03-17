
using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GenreModels;
using GamingVIVE.Services.GamesServices;
using GamingVIVE_MVC.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;
        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GenreListItem> CreateGenre(GenreCreate genreCreate)
        {

            var list = new GenreListItem();
            var genre = new Genre()
            {
                Name = genreCreate.Name
            };

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            list.Name = genre.Name;
            list.GenreId = genre.GenreId;

            return list;
        }

        public async Task<bool> DeleteGenre(int genreId)
        {
            var genre = await _context.Genres.FindAsync(genreId);

            if (genre == null)
                return false;


            _context.Genres.Remove(genre);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> EditGenre(GenreEdit genreEdit)
        {
            var genre = await _context.Genres.FindAsync(genreEdit.GenreId);

            if (genre == null)
                return false;

            genre.Name = genreEdit.Name;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<List<GenreListItem>> GetAllGenres()
        {
            return await _context.Genres.Select(entity => new GenreListItem
            {
                GenreId = entity.GenreId,
                Name = entity.Name,
            }).ToListAsync();
        }


        public async Task<GenreDetails> GetGenreByName(string search)
        {
            var genre = await _context.Genres.Include(c => c.Genres).ThenInclude(g => g.Game).FirstOrDefaultAsync(i => i.Name == search);
            if (genre is null)
            {
                return null;
            }

            return new GenreDetails
            {
                GenreId = genre.GenreId,
                Name = genre.Name,
                GenreGames = genre.Genres.Select(g => g.Game).Select(gl => new Game
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

        public async Task<GenreDetails> GetGenreById(int genreId)
        {
            var genre = await _context.Genres.Include(c => c.Genres).ThenInclude(g => g.Game).FirstOrDefaultAsync(i => i.GenreId == genreId);
            if (genre is null)
            {
                return null;
            }

            return new GenreDetails
            {
                GenreId = genre.GenreId,
                Name = genre.Name,
                GenreGames = genre.Genres.Select(g => g.Game).Select(gl => new Game
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
