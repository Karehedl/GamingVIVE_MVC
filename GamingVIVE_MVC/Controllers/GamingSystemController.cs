using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GameModels;
using GamingVIVE.Models.GamingSystemModels;
using GamingVIVE.Services.GameService;
using GamingVIVE.Services.GamesServices;
using GamingVIVE.Services.GamingSystemService;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace GamingVIVE_MVC.Controllers
{
    public class GamingSystemController : Controller
    {
        private IGamingSystemService _gamingSystemService;


        public GamingSystemController(IGamingSystemService gamingSystemService)
        {
            _gamingSystemService = gamingSystemService;
        }

        //Public

        [HttpGet]
        [Route("GamingSystem/Systems")]
        public async Task<IActionResult> Index()
        {
            var gsList = await _gamingSystemService.GetAllGamingSystems();
            return View(gsList);
        }


        [HttpGet]
        public async Task<IActionResult> GetGamingSystemById(int gamingSystemId)
        {
            return View(await _gamingSystemService.GetGamingSystemById(gamingSystemId));
        }


        [HttpGet]
        public async Task<IActionResult> GetGamingSystemByName(string search)
        {
            return View(await _gamingSystemService.GetGamingSystemByName(search));
        }


        //Settings

        [HttpGet]
        [Route("GamingSystem/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("GamingSystem/Create")]
        public async Task<IActionResult> Create(GamingSystemCreate gamingSystemCreate)
        {

            if (!ModelState.IsValid) return View(gamingSystemCreate);

            await _gamingSystemService.CreateGamingSystem(gamingSystemCreate);
            return RedirectToAction(nameof(GamingSystemSettings));

        }


        [HttpGet]
        [Route("GamingSystem/Edit")]
        public async Task<IActionResult> EditGamingSystem(int gamingSystemId)
        {
            var gs = await _gamingSystemService.GetGamingSystemById(gamingSystemId);
            var gsEdit = new GamingSystemEdit
            {
                Name = gs.Name,
            };

            return View(gsEdit);

        }

        [HttpPost]
        [Route("GamingSystem/Edit")]
        public async Task<IActionResult> EditGamingSystem(int gameId, GamingSystemEdit gamingSystemEdit)
        {

            if (await _gamingSystemService.EditGamingSystem(gamingSystemEdit))
                return RedirectToAction(nameof(GamingSystemSettings));
            else
                return View(gamingSystemEdit);

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(await _gamingSystemService.EditGamingSystem(gamingSystemEdit));

        }


        [HttpGet]
        [Route("GamingSystem/Delete/{GamingSystemId}")]
        public async Task<IActionResult> DeleteGamingSystem(int? gamingSystemId)
        {
            if (gamingSystemId > 0)
            {
                var gs = await _gamingSystemService.GetGamingSystemById(gamingSystemId.Value);
                if (gs != null)
                {
                    return View(gs);
                }
            }

            return View();

        }

        [HttpPost]
        [Route("GamingSystem/Delete/{GamingSystemId}")]
        public async Task<IActionResult> DeleteGamingSystem(int gamingSystemId)
        {
            var IsSuccessful = await _gamingSystemService.DeleteGamingSystem(gamingSystemId);
            if (IsSuccessful)
            {
                return RedirectToAction(nameof(GamingSystemSettings));
            }
            else
                return UnprocessableEntity();

        }

        [HttpGet]
        public async Task<IActionResult> GamingSystemSettings()
        {
            var gsList = await _gamingSystemService.GetAllGamingSystems();
            return View(gsList);
        }

        [HttpGet]
        public async Task<IActionResult> GetGamingSystemByNameSettings(string search)
        {
            return View(await _gamingSystemService.GetGamingSystemByName(search));
        }

        [HttpGet]
        public async Task<IActionResult> GetGamingSystemByIdSettings(int gamingSystemId)
        {
            return View(await _gamingSystemService.GetGamingSystemById(gamingSystemId));
        }
    }
}
