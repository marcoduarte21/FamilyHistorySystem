using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FamilyHistorySystem.DataAccess;
using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Services.services;
using FamilyHistorySystem.Services.interfaces;
using AutoMapper;
using FamilyHistorySystem.Utils.constants.Response;
using FamilyHistorySystem.Utils.constants.messages.successMessage;
using FamilyHistorySystem.Models.DTOs;

namespace FamilyHistorySystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentsFamilyHistoryController : ControllerBase
    {
        private readonly IFamilyHistory _familyHistoryService;

        public StudentsFamilyHistoryController(IFamilyHistory familyHistoryService)
        {
            _familyHistoryService = familyHistoryService;
        }

        [HttpGet("getParents/{cedula}")]
        public async Task<IActionResult> GetParents(string cedula)
        {
            var parents = await _familyHistoryService.GetParents(cedula);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.ParentsFound, parents));
        }

        [HttpGet("getChildren/{cedula}")]
        public async Task<IActionResult> GetChildren(string cedula)
        {
            var children = await _familyHistoryService.GetChildren(cedula);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.ChildrenFound, children));
        }

        [HttpGet("getSiblings/{cedula}")]
        public async Task<IActionResult> GetSiblings(string cedula)
        {
            var siblings = await _familyHistoryService.GetSiblings(cedula);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.SiblingsFound, siblings));
        }

        [HttpGet("getGrandParents/{cedula}")]
        public async Task<IActionResult> GetGrandParents(string cedula)
        {
            var grandparents = await _familyHistoryService.GetGrandParents(cedula);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.GrandParentsFound, grandparents));
        }

        [HttpGet("getUncles/{cedula}")]
        public async Task<IActionResult> GetUncles(string cedula)
        {
            var uncles = await _familyHistoryService.GetUncles(cedula);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.UnclesFound, uncles));
        }

        [HttpGet("getCousins/{cedula}")]
        public async Task<IActionResult> GetCousins(string cedula)
        {
            var cousins = await _familyHistoryService.GetCousins(cedula);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.CousinsFound, cousins));
        }
    }
}
