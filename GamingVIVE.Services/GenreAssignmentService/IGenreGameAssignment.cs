using GamingVIVE.Models.GenreGameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.GenreAssignmentService
{
    public interface IGenreGameAssignment
    {
        Task<bool> AssignGameToGenres(GenreGameCreate model);
        Task<List<GenreGameListItem>> GetAllGenreConnections();
        Task<GenreGameDetails> GetGenreConnectionById(int Id);
        Task<bool> DeleteGenreConnection(int Id);
        Task<bool> EditGenreConnection(GenreGameEdit genreGameEdit);

    }
}
