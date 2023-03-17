using GamingVIVE.Data.Entities;
using GamingVIVE.Models.MontizedCategoryModels;
using GamingVIVE.Services.MontizedCategoryService;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace GamingVIVE_MVC.Controllers
{
    public class MontizedCategoryController : Controller
    {
        private IMontizedCategoryService _categoryService;


        public MontizedCategoryController(IMontizedCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //Public

        [HttpGet]
        [Route("Category")]
        public async Task<IActionResult> Index()
        {
            var gsList = await _categoryService.GetAllMontizedCategories();
            return View(gsList);
        }


        [HttpGet]
        public async Task<IActionResult> GetMontizedCategoryById(int montizedCategoryId)
        {
            return View(await _categoryService.GetMontizedCategoryById(montizedCategoryId));
        }


        [HttpGet]
        public async Task<IActionResult> GetMontizedCategoryByName(string search)
        {
            return View(await _categoryService.GetMontizedCategoryByName(search));
        }


        //Settings

        [HttpGet]
        [Route("Category/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Category/Create")]
        public async Task<IActionResult> CreateMontizedCategory(MontizedCategoryCreate montizedCategoryCreate)
        {
            if (!ModelState.IsValid) return View(montizedCategoryCreate);

            await _categoryService.CreateMontizedCategory(montizedCategoryCreate);
            return RedirectToAction(nameof(MontizedCategorySettings));

        }


        [HttpGet]
        [Route("Category/Edit")]
        public async Task<IActionResult> EditMontizedCategory(int montizedCategoryId)
        {
            var mc = await _categoryService.GetMontizedCategoryById(montizedCategoryId);
            var mcEdit = new MontizedCategoryEdit
            {
                Name = mc.Name,
            };

            return View(mcEdit);

        }

        [HttpPost]
        [Route("Category/Edit")]
        public async Task<IActionResult> EditMontizedCategory(int montizedCategoryId, MontizedCategoryEdit montizedCategoryEdit)
        {

            if (await _categoryService.EditMontizedCategory(montizedCategoryEdit))
                return RedirectToAction(nameof(MontizedCategorySettings));
            else
                return View(montizedCategoryEdit);

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(await _categoryService.EditMontizedCategory(montizedCategoryEdit));

        }


        [HttpGet("montizedCategoryId")]
        [Route("Category/Delete/{MontizedCategoryId}")]
        public async Task<IActionResult> DeleteMontizedCategory(int? montizedCategoryId)
        {
            if (montizedCategoryId > 0)
            {
                var mc = await _categoryService.GetMontizedCategoryById(montizedCategoryId.Value);
                if (mc != null)
                {
                    return View(mc);
                }
            }

            return View();

        }

        [HttpPost]
        [Route("Category/Delete/{MontizedCategoryId}")]
        public async Task<IActionResult> DeleteMontizedCategory(int montizedCategoryId)
        {
            var IsSuccessful =  await _categoryService.DeleteMontizedCategory(montizedCategoryId);
            if (IsSuccessful)
            {
                return RedirectToAction(nameof(MontizedCategorySettings));
            }
            else
                return UnprocessableEntity();

        }

        [HttpGet]
        public async Task<IActionResult> MontizedCategorySettings()
        {
            var mcList = await _categoryService.GetAllMontizedCategories();
            return View(mcList);
        }

        [HttpGet]
        public async Task<IActionResult> GetMontizedCategoryByNameSettings(string search)
        {
            return View(await _categoryService.GetMontizedCategoryByName(search));
        }

        [HttpGet]
        public async Task<IActionResult> GetMontizedCategoryByIdSettings(int montizedCategoryId)
        {
            return View(await _categoryService.GetMontizedCategoryById(montizedCategoryId));
        }
    }
}
