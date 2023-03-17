using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GamingSystemModels;
using GamingVIVE.Models.GenreModels;
using GamingVIVE.Services.GamingSystemService;
using GamingVIVE.Services.GenreService;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace GamingVIVE_MVC.Controllers
{
    public class GenreController : Controller
    {
        private IGenreService _genreService;


        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        //Public

        [HttpGet]
        [Route("Genre")]
        public async Task<IActionResult> Index()
        {
            var genreList = await _genreService.GetAllGenres();
            return View(genreList);
        }

        [HttpGet]
        public async Task<IActionResult> GetGenreById(int genreId)
        {
            return View(await _genreService.GetGenreById(genreId));
        }

        [HttpGet]
        public async Task<IActionResult> GetGenreByName(string search)
        {
            return View(await _genreService.GetGenreByName(search));
        }

        //Settings

        [HttpGet]
        public async Task<IActionResult> GenreSettings()
        {
            var genreList = await _genreService.GetAllGenres();
            return View(genreList);
        }

        [HttpGet]
        [Route("Genre/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Genre/Create")]
        public async Task<IActionResult> CreateGenre(GenreCreate genreCreate)
        {

            if (!ModelState.IsValid) return View(genreCreate);

            await _genreService.CreateGenre(genreCreate);
            return RedirectToAction(nameof(GenreSettings));

        }


        [HttpGet]
        [Route("Genre/Edit")]
        public async Task<IActionResult> EditGenre(int genreId)
        {
            var g = await _genreService.GetGenreById(genreId);
            var gEdit = new GenreEdit
            {
                Name = g.Name,
            };

            return View(gEdit);

        }

        [HttpPost]
        [Route("Genre/Edit")]
        public async Task<IActionResult> EditGenre(int gameId, GenreEdit genreEdit)
        {

            if (await _genreService.EditGenre(genreEdit))
                return RedirectToAction(nameof(GenreSettings));
            else
                return View(genreEdit);

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(await _genreService.EditGenre(genreEdit));

        }


        [HttpGet("genreId")]
        [Route("Genre/Delete/{GenreId}")]
        public async Task<IActionResult> DeleteGenre(int? genreId)
        {
            if (genreId > 0)
            {
                var genre = await _genreService.GetGenreById(genreId.Value);
                if (genre != null)
                {
                    return View(genre);
                }
            }

            return View();

        }

        [HttpPost]
        [Route("Genre/Delete/{GenreId}")]
        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            var IsSuccessful = await _genreService.DeleteGenre(genreId);
            if (IsSuccessful)
            {
                return RedirectToAction(nameof(GenreSettings));
            }
            else
                return UnprocessableEntity();

        }

        [HttpGet]
        public async Task<IActionResult> GetGenreByIdSettings(int genreId)
        {
            return View(await _genreService.GetGenreById(genreId));
        }

        [HttpGet]
        public async Task<IActionResult> GetGenreByNameSettings(string search)
        {
            return View(await _genreService.GetGenreByName(search));
        }
    }
}
