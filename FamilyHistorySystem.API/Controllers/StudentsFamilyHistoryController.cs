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
using Microsoft.AspNetCore.Authorization;

namespace FamilyHistorySystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class StudentsFamilyHistoryController : ControllerBase
    {
        private readonly IFamilyHistory _familyHistoryService;

        public StudentsFamilyHistoryController(IFamilyHistory familyHistoryService)
        {
            _familyHistoryService = familyHistoryService;
        }

        [HttpGet("getParents/{nationalId}")]
        public async Task<IActionResult> GetParents(string nationalId)
        {
            var parents = await _familyHistoryService.GetParents(nationalId);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.ParentsFound, parents));
        }

        [HttpGet("getChildren/{nationalId}")]
        public async Task<IActionResult> GetChildren(string nationalId)
        {
            var children = await _familyHistoryService.GetChildren(nationalId);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.ChildrenFound, children));
        }

        [HttpGet("getSiblings/{nationalId}")]
        public async Task<IActionResult> GetSiblings(string nationalId)
        {
            var siblings = await _familyHistoryService.GetSiblings(nationalId);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.SiblingsFound, siblings));
        }

        [HttpGet("getGrandParents/{nationalId}")]
        public async Task<IActionResult> GetGrandParents(string nationalId)
        {
            var grandparents = await _familyHistoryService.GetGrandParents(nationalId);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.GrandParentsFound, grandparents));
        }

        [HttpGet("getUncles/{nationalId}")]
        public async Task<IActionResult> GetUncles(string nationalId)
        {
            var uncles = await _familyHistoryService.GetUncles(nationalId);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.UnclesFound, uncles));
        }

        [HttpGet("getCousins/{nationalId}")]
        public async Task<IActionResult> GetCousins(string nationalId)
        {
            var cousins = await _familyHistoryService.GetCousins(nationalId);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(SuccessMessage.CousinsFound, cousins));
        }
    }
}
