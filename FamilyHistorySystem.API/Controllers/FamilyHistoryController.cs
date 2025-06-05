using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FamilyHistorySystem.DataAccess;
using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Services.services;

namespace FamilyHistorySystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FamilyHistoryStudentController(DBContexto context) : ControllerBase
    {
        DBContexto _context = context;
        FamilyHistoryService FamilyHistoryService = new(context);

        [HttpGet("getParents/{cedula}")]
        public async Task<IActionResult> GetParents(string cedula)
        {
            var parents = await FamilyHistoryService.GetParents(cedula);
            return Ok(parents);

        }

        [HttpGet("getChildren/{cedula}")]
        public async Task<IActionResult> GetChildren(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
            {
                return BadRequest("Cedula cannot be null or empty.");
            }
            var children = await FamilyHistoryService.GetChildren(cedula);
            return Ok(children);
        }

        [HttpGet("getSiblings/{cedula}")]
        public async Task<IActionResult> GetSiblings(string cedula)
        {
            var siblings = await FamilyHistoryService.GetSiblings(cedula);
            return Ok(siblings);
        }

        [HttpGet("getGrandParents/{cedula}")]
        public async Task<IActionResult> GetGrandParents(string cedula)
        {
            var grandparents = await FamilyHistoryService.GetGrandParents(cedula);
            return Ok(grandparents);
        }

        [HttpGet("getUncles/{cedula}")]
        public async Task<IActionResult> GetUncles(string cedula)
        {
            var uncles = await FamilyHistoryService.GetUncles(cedula);
            return Ok(uncles);
        }

        [HttpGet("getCousins/{cedula}")]
        public async Task<IActionResult> GetCousins(string cedula)
        {
            var cousins = await FamilyHistoryService.GetCousins(cedula);
            return Ok(cousins);
        }

    }
}
