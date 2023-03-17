using GamingVIVE.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingVIVE.Services.GamesServices
{
    public interface IGameService
    {
        public Task<bool> GameCreateAsync(GameCreate game);
        public Task<List<GameListItem>> GetAllGames();
        public Task<bool> DeleteGame(int gameId);

        public Task<bool> EditGame(GameEdit gameEdit);

        public Task<GameDetails> GetGameByName(string search);

        public Task<GameDetails> GetGameById(int gameId);

    }
}
