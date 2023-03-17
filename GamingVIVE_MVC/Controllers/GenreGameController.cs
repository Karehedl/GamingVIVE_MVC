using GamingVIVE.Models.GenreGameModels;
using GamingVIVE.Services.GamingSystemAssignmentService;
using GamingVIVE.Services.GenreAssignmentService;
using Microsoft.AspNetCore.Mvc;

namespace GamingVIVE_MVC.Controllers
{
    public class GenreGameController : Controller
    {
        private IGenreGameAssignment _genreGameAssignment;


        public GenreGameController(IGenreGameAssignment genreGameAssignment)
        {
            _genreGameAssignment = genreGameAssignment;
        }


        [HttpGet]
        public async Task<IActionResult> GenreConnectionSettings()
        {
            var gsList = await _genreGameAssignment.GetAllGenreConnections();
            return View(gsList);
        }

        [HttpGet]
        [Route("GenreConnection/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("GenreConnection/Create")]
        public async Task<IActionResult> AssignGameToGenre(GenreGameCreate model)
        {

            if (!ModelState.IsValid) return View(model);

            await _genreGameAssignment.AssignGameToGenres(model);
            return RedirectToAction(nameof(GenreConnectionSettings));
        }

        [HttpGet]
        [Route("GenreConnection/Edit/{Id}")]
        public async Task<IActionResult> EditGenreConnection(int Id)
        {
            var genre = await _genreGameAssignment.GetGenreConnectionById(Id);
            var genreEdit = new GenreGameEdit
            {
                GameId = genre.GameId,
                GenreId = genre.GenreId,
            };

            return View(genreEdit);

        }

        [HttpPost]
        [Route("GenreConnection/Edit/{Id}")]
        public async Task<IActionResult> EditConnection(int genreId, GenreGameEdit genreGameEdit)
        {

            if (await _genreGameAssignment.EditGenreConnection(genreGameEdit))
                return RedirectToAction(nameof(GenreConnectionSettings));
            else
                return View(genreGameEdit);

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(await _genreGameAssignment.EditGenreConnection(genreGameEdit));

        }


        [HttpGet]
        [Route("GenreConnection/Delete/{Id}")]
        public async Task<IActionResult> DeleteGenreConnection(int? Id)
        {
            if (Id > 0)
            {
                var genre= await _genreGameAssignment.GetGenreConnectionById(Id.Value);
                if (genre != null)
                {
                    return View(genre);
                }
            }

            return View();
        }

        [HttpPost]
        [Route("GenreConnection/Delete/{Id}")]
        public async Task<IActionResult> DeleteGenreConnection(int Id)
        {
            var IsSuccessful = await _genreGameAssignment.DeleteGenreConnection(Id);
            if (IsSuccessful)
            {
                return RedirectToAction(nameof(GenreConnectionSettings));
            }
            else
                return UnprocessableEntity();

        }

    }
}
