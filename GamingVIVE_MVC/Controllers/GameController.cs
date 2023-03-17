using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GameModels;
using GamingVIVE.Services.GameService;
using GamingVIVE.Services.GamesServices;
using GamingVIVE_MVC.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GamingVIVE_MVC.Controllers
{
    public class GameController : Controller
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /*Public*/

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var gList = await _gameService.GetAllGames();
            return View(gList);
        }

        [HttpGet]
        public async Task<IActionResult> GetGameByName(string search)
        {
            return View(await _gameService.GetGameByName(search));
        }

        [HttpGet]
        public async Task<IActionResult> GetGameById(int gameId)
        {
            return View(await _gameService.GetGameById(gameId));
        }

        /*Settings*/

        [HttpGet]  
        [Route("/Games/Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Route("/Games/Create")]
        public async Task<IActionResult> Create(GameCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            await _gameService.GameCreateAsync(model);
            return RedirectToAction(nameof(GameSettings));

        }


        [HttpGet]
        [Route("/Games/Edit")]
        public async Task<IActionResult> EditGame(int gameId)
        {
            var game = await _gameService.GetGameById(gameId);
            var gameEdit = new GameEdit
            {
                Name= game.Name,
                Rating = game.Rating,
                Description = game.Description,
                Picture = game.Picture,
                Publisher = game.Publisher,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            }; 

            return View(gameEdit);

        }

        [HttpPost]
        [Route("/Games/Edit")]
        public async Task<IActionResult> EditGame(int gameId, GameEdit gameEdit)
        {
         
           if (await _gameService.EditGame(gameEdit))
                return RedirectToAction(nameof(Index));
            else
                return View(gameEdit);

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(await _gameService.EditGame(gameEdit));

        }

        [HttpGet]
        [Route("/Games/Delete/{GameId}")]
        public async Task<IActionResult> DeleteGame(int? gameId)
        {
            if (gameId > 0)
            {
                var game = await _gameService.GetGameById(gameId.Value);
                if (game != null)
                {
                    return View(game);
                }
            }

            return View();

        }

        [HttpPost]
        [Route("/Games/Delete/{GameId}")]
        public async Task<IActionResult> DeleteGame(int gameId)
        {
            var IsSuccessful = await _gameService.DeleteGame(gameId);
            if (IsSuccessful)
            {
                return RedirectToAction(nameof(GameSettings));
            }
            else
                return UnprocessableEntity();

        }


        [HttpGet]
        public async Task<IActionResult> GameSettings()
        {
            var gList = await _gameService.GetAllGames();
            return View(gList);
        }

        [HttpGet]
        public async Task<IActionResult> GetGameByNameSettings(string search)
        {
            return View(await _gameService.GetGameByName(search));
        }

        [HttpGet]
        public async Task<IActionResult> GetGameByIdSettings(int gameId)
        {
            return View(await _gameService.GetGameById(gameId));
        }
    }
}
