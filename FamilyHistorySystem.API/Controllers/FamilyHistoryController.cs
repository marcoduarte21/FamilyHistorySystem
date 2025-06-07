using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FamilyHistorySystem.DataAccess;
using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Services.services;
using FamilyHistorySystem.Services.interfaces;
using AutoMapper;

namespace FamilyHistorySystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FamilyHistoryStudentController(DBContexto context, IMapper mapper) : ControllerBase
    {
        private readonly DBContexto _context = context;
        private readonly FamilyHistoryService _familyHistoryService = new(context,mapper);

        [HttpGet("getParents/{cedula}")]
        public async Task<IActionResult> GetParents(string cedula)
        {
            var parents = await _familyHistoryService.GetParents(cedula);
            return Ok(parents);
        }

        [HttpGet("getChildren/{cedula}")]
        public async Task<IActionResult> GetChildren(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
            {
                return BadRequest("Cedula cannot be null or empty.");
            }
            var children = await _familyHistoryService.GetChildren(cedula);
            return Ok(children);
        }

        [HttpGet("getSiblings/{cedula}")]
        public async Task<IActionResult> GetSiblings(string cedula)
        {
            var siblings = await _familyHistoryService.GetSiblings(cedula);
            return Ok(siblings);
        }

        [HttpGet("getGrandParents/{cedula}")]
        public async Task<IActionResult> GetGrandParents(string cedula)
        {
            var grandparents = await _familyHistoryService.GetGrandParents(cedula);
            return Ok(grandparents);
        }

        [HttpGet("getUncles/{cedula}")]
        public async Task<IActionResult> GetUncles(string cedula)
        {
            var uncles = await _familyHistoryService.GetUncles(cedula);
            return Ok(uncles);
        }

        [HttpGet("getCousins/{cedula}")]
        public async Task<IActionResult> GetCousins(string cedula)
        {
            var cousins = await _familyHistoryService.GetCousins(cedula);
            return Ok(cousins);
        }
    }
}
