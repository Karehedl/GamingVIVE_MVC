using GamingVIVE.Models.GenreGameModels;
using GamingVIVE.Models.MCGameModels;
using GamingVIVE.Services.GenreAssignmentService;
using GamingVIVE.Services.MontizedCategoryAssignmentService;
using Microsoft.AspNetCore.Mvc;

namespace GamingVIVE_MVC.Controllers
{
    public class MontizedCategoryGameController : Controller
    {
        private IMontizedCategoryGameAssignment _categoryGameAssignment;


        public MontizedCategoryGameController(IMontizedCategoryGameAssignment montizedCategoryGameAssignment)
        {
            _categoryGameAssignment = montizedCategoryGameAssignment;
        }


        [HttpGet]
        [Route("CategoryConnection")]
        public async Task<IActionResult> CategoryConnectionSettings()
        {
            var gsList = await _categoryGameAssignment.GetAllConnections();
            return View(gsList);
        }

        [HttpGet]
        [Route("CategoryConnection/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("CategoryConnection/Create")]
        public async Task<IActionResult> AssignGameToCategory(MontizedCategoryGameCreate model)
        {

            if (!ModelState.IsValid) return View(model);

            await _categoryGameAssignment.AssignGameToCategory(model);
            return RedirectToAction(nameof(CategoryConnectionSettings));
        }

        [HttpGet]
        [Route("CategoryConnection/Edit/{Id}")]
        public async Task<IActionResult> EditCategoryConnection(int Id)
        {
            var gs = await _categoryGameAssignment.GetConnectionById(Id);
            var gsEdit = new MontizedCategoryGameEdit
            {
                GameId = gs.GameId,
                MontizedCategoryId = gs.MontizedCategoryId,
            };

            return View(gsEdit);

        }

        [HttpPost]
        [Route("CategoryConnection/Edit/{Id}")]
        public async Task<IActionResult> EditCategoryConnection(int Id, MontizedCategoryGameEdit montizedCategoryGameEdit)
        {

            if (await _categoryGameAssignment.EditConnection(montizedCategoryGameEdit))
                return RedirectToAction(nameof(CategoryConnectionSettings));
            else
                return View(montizedCategoryGameEdit);

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(await _categoryGameAssignment.EditConnection(montizedCategoryGameEdit));

        }


        [HttpGet]
        [Route("CategoryConnection/Delete/{Id}")]
        public async Task<IActionResult> DeleteCategoryConnection(int? Id)
        {
            if (Id > 0)
            {
                var gs = await _categoryGameAssignment.GetConnectionById(Id.Value);
                if (gs != null)
                {
                    return View(gs);
                }
            }

            return View();
        }

        [HttpPost]
        [Route("CategoryConnection/Delete/{Id}")]
        public async Task<IActionResult> DeleteCategoryConnection(int Id)
        {
            var IsSuccessful = await _categoryGameAssignment.DeleteConnection(Id);
            if (IsSuccessful)
            {
                return RedirectToAction(nameof(CategoryConnectionSettings));
            }
            else
                return UnprocessableEntity();

        }
    }
}
