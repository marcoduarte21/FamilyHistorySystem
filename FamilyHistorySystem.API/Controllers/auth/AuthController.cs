
using AutoMapper;
using FamilyHistorySystem.Models.DTOs.Auth;
using FamilyHistorySystem.Services.interfaces;
using FamilyHistorySystem.Services.interfaces.auth;
using FamilyHistorySystem.Utils.constants.messages.ErrorMessage;
using FamilyHistorySystem.Utils.constants.messages.successMessage;
using FamilyHistorySystem.Utils.constants.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHistorySystem.API.Controllers.auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IMapper mapper, IUserService userService)
        {
            _authService = authService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _authService.RegisterAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id },
                new ApiResponse<RegisterResponseDto>(SuccessMessage.RegisterSuccess, user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _authService.LoginAsync(dto);
            return Ok(new ApiResponse<LoginResponseDto>(SuccessMessage.LoginSuccess, user));
        }

        [HttpPost("logout/{id}")]
        public async Task<IActionResult> Logout(Guid id)
        {
            var success = await _authService.LogoutAsync(id);
            if (success)
            {
                return Ok(new ApiResponse<object>(SuccessMessage.LogoutSuccess, success));
            }
            return BadRequest(new ApiResponse<object>(ErrorMessage.LogoutFailed, null, false));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest(new ApiResponse<object>(ErrorMessage.InvalidToken, null, false));
            }
            var newToken = await _authService.RefreshTokenAsync(refreshToken);
            return Ok(new ApiResponse<object>(SuccessMessage.tokenRefreshed, newToken));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdOrThrowAsync(id);
            return Ok(new ApiResponse<RegisterResponseDto>(SuccessMessage.UserFound,
                _mapper.Map<RegisterResponseDto>(user)));
        }

        [HttpGet("GetByEmail/{email}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmailOrThrowAsync(email);
            return Ok(new ApiResponse<RegisterResponseDto>(SuccessMessage.UserFound,
                _mapper.Map<RegisterResponseDto>(user)));
        }
    }
}
