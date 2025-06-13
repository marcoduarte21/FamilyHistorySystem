using AutoMapper;
using FamilyHistorySystem.DataAccess;
using FamilyHistorySystem.Exceptions;
using FamilyHistorySystem.Models.DTOs.Auth;
using FamilyHistorySystem.Models.Entities.Auth;
using FamilyHistorySystem.Services.interfaces.auth;
using FamilyHistorySystem.Utils.constants.messages.ErrorMessage;
using FamilyHistorySystem.Utils.constants.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using FamilyHistorySystem.Services.interfaces;

namespace FamilyHistorySystem.Services.services.auth
{
    public class AuthService : IAuthService
    {
        private readonly DBContexto _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AuthService(DBContexto context, IMapper mapper, IConfiguration config, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _userService = userService;

        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x =>
            x.Email == dto.Email);

            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(dto.Password, existingUser.PasswordHash))
            {
                throw new CustomException(ErrorMessage.InvalidCredentials, StatusCode.Unauthorized);
            }

            var accessToken = GenerateAccessToken(existingUser);
            var refreshToken = GenerateRefreshToken();

            existingUser.RefreshToken = refreshToken;
            existingUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = _mapper.Map<RegisterResponseDto>(existingUser)
            };

        }

        public async Task<bool> LogoutAsync(Guid id)
        {
            User user = await _userService.GetByIdOrThrowAsync(id);

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            _context.Users.Update(user);

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<object> RefreshTokenAsync(string token)
        {
            User existingUserSession = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == token
            && x.RefreshTokenExpiryTime > DateTime.UtcNow) ??
                throw new CustomException(ErrorMessage.InvalidToken, StatusCode.Unauthorized);

            return new
            {
                AccessToken = GenerateAccessToken(existingUserSession),
                RefreshToken = existingUserSession.RefreshToken,
                User = _mapper.Map<RegisterResponseDto>(existingUserSession)
            };
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterUserDto dto)
        {
            if (dto.Role == Role.user)
            {

                var user = await _userService.CreateUserAsync(dto);
                return user;
            }
            else
            {
                throw new CustomException(ErrorMessage.RegisterOnlyUserRole, StatusCode.BadRequest);
            }
        }


        private string GenerateAccessToken(User user)
        {
            
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(128));
        }
    }
}
