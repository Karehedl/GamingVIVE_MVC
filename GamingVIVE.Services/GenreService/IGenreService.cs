
using GamingVIVE.Models.GenreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.GenreService
{
    public interface IGenreService
    {
        public Task<GenreListItem> CreateGenre(GenreCreate genreCreate);

        public Task<List<GenreListItem>> GetAllGenres();

        public Task<bool> DeleteGenre(int genreId);

        public Task<bool> EditGenre(GenreEdit genreEdit);

        public Task<GenreDetails> GetGenreByName(string search);

        public Task<GenreDetails> GetGenreById(int genreId);
    }
}
