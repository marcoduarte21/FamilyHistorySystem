using FamilyHistorySystem.Models.DTOs.Auth;
using FamilyHistorySystem.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Services.interfaces.auth
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
        Task<RegisterResponseDto> RegisterAsync(RegisterUserDto dto);
        Task<bool> LogoutAsync(Guid id);
        Task<object> RefreshTokenAsync(string token);
        Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
    }
}
