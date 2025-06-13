using AutoMapper;
using FamilyHistorySystem.Models.DTOs.Auth;
using FamilyHistorySystem.Models.DTOs.user;
using FamilyHistorySystem.Models.Entities.Auth;
using FamilyHistorySystem.Services.interfaces;
using FamilyHistorySystem.Utils.constants.messages.successMessage;
using FamilyHistorySystem.Utils.constants.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FamilyHistorySystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] RegisterUserDto dto)
        {
            var user = await _userService.CreateUserAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new ApiResponse<RegisterResponseDto>
                (SuccessMessage.RegisterSuccess, user));

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdOrThrowAsync(id);

            return Ok(new ApiResponse<UserResponseDto>(SuccessMessage.UserFound,
                _mapper.Map<UserResponseDto>(user)));
        }

        [HttpGet("GetByEmail/{email}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmailOrThrowAsync(email);
            return Ok(new ApiResponse<UserResponseDto>(SuccessMessage.UserFound,
                _mapper.Map<UserResponseDto>(user)));
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(new ApiResponse<List<UserResponseDto>>(SuccessMessage.UsersFound, users));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserRequestDto dto)
        {
            var user = await _userService.UpdateUserAsync(id, dto);
            return Ok(new ApiResponse<UserResponseDto>(SuccessMessage.UserFound, user));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);

            return Ok(new ApiResponse<bool>(SuccessMessage.UserFound, result));

        }

        [HttpGet("GetProfile")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetProfile()
        {
            var userLoggedId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userLoggedId == null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetByIdOrThrowAsync(Guid.Parse(userLoggedId));

            return Ok(new ApiResponse<UserResponseDto>(SuccessMessage.UserFound,
                _mapper.Map<UserResponseDto>(user)));

        }
    }
}
