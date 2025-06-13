using FamilyHistorySystem.Models.DTOs.Auth;
using FamilyHistorySystem.Models.DTOs.user;
using FamilyHistorySystem.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Services.interfaces
{
    public interface IUserService
    {
        public Task<List<UserResponseDto>> GetAllUsersAsync();
        public Task<User> GetByIdAsync(Guid id);
        public Task<RegisterResponseDto> CreateUserAsync(RegisterUserDto userDto);
        public Task<UserResponseDto> UpdateUserAsync(Guid id, UserRequestDto userDto);
        public Task<bool> DeleteUserAsync(Guid id);
        public Task<User> GetByEmailAsync(string email);

        public Task<User> GetByIdOrThrowAsync(Guid id);

        public Task<User> GetByEmailOrThrowAsync(string email);


    }
}
