using GamingVIVE.Data.Entities;
using GamingVIVE.Models.GamingSystemModels;
using GamingVIVE.Models.GSGameModels;
using GamingVIVE.Services.GamingSystemAssignmentService;
using GamingVIVE.Services.GamingSystemService;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace GamingVIVE_MVC.Controllers
{
    public class GamingSystemGameController : Controller
    {
        private IGamingSystemGameAssignment _gamingSystemGameAssignment;


        public GamingSystemGameController(IGamingSystemGameAssignment gamingSystemGameAssignment)
        {
            _gamingSystemGameAssignment = gamingSystemGameAssignment;
        }


        [HttpGet]
        public async Task<IActionResult> ConnectionSettings()
        {
            var gsList = await _gamingSystemGameAssignment.GetAllSystemConnections();
            return View(gsList);
        }

        [HttpGet]
        [Route("Connection/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Connection/Create")]
        public async Task<IActionResult> AssignGameToSystem(GamingSystemGameCreate model)
        {

            if (!ModelState.IsValid) return View(model);

            await _gamingSystemGameAssignment.AssignGameToSystem(model);
            return RedirectToAction(nameof(ConnectionSettings));
        }

        [HttpGet]
        [Route("Connection/Edit/{Id}")]
        public async Task<IActionResult> EditConnection(int Id)
        {
            var gs = await _gamingSystemGameAssignment.GetSystemConnectionById(Id);
            var gsEdit = new GamingSystemGameEdit
            {
                GameId = gs.GameId,
                GamingSystemId= gs.GamingSystemId,
            };

            return View(gsEdit);

        }

        [HttpPost]
        [Route("Connection/Edit/{Id}")]
        public async Task<IActionResult> EditConnection(int Id, GamingSystemGameEdit gamingSystemGameEdit)
        {

            if (await _gamingSystemGameAssignment.EditSystemConnection(gamingSystemGameEdit))
                return RedirectToAction(nameof(ConnectionSettings));
            else
                return View(gamingSystemGameEdit);

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(await _gamingSystemGameAssignment.EditSystemConnection(gamingSystemGameEdit));

        }


        [HttpGet("Id")]
        [Route("Connection/Delete/{Id}")]
        public async Task<IActionResult> DeleteConnection(int? Id)
        {
            if (Id > 0)
            {
                var gs = await _gamingSystemGameAssignment.GetSystemConnectionById(Id.Value);
                if (gs != null)
                {
                    return View(gs);
                }
            }

            return View();
        }

        [HttpPost]
        [Route("Connection/Delete/{Id}")]
        public async Task<IActionResult> DeleteConnection(int Id)
        {
            var IsSuccessful = await _gamingSystemGameAssignment.DeleteSystemConnection(Id);
            if (IsSuccessful)
            {
                return RedirectToAction(nameof(ConnectionSettings));
            }
            else
                return UnprocessableEntity();

        }

    }
}
