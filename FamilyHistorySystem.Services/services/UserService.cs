using AutoMapper;
using FamilyHistorySystem.DataAccess;
using FamilyHistorySystem.Exceptions;
using FamilyHistorySystem.Models.DTOs.Auth;
using FamilyHistorySystem.Models.DTOs.user;
using FamilyHistorySystem.Models.Entities.Auth;
using FamilyHistorySystem.Services.interfaces;
using FamilyHistorySystem.Utils.constants.messages.ErrorMessage;
using FamilyHistorySystem.Utils.constants.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Services.services
{
    public class UserService : IUserService
    {
        DBContexto _context;
        IMapper _mapper;
        public UserService(DBContexto context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RegisterResponseDto> CreateUserAsync(RegisterUserDto userDto)
        {
            if (userDto.Role == Role.user)
            {
                var existingUser = await GetByEmailAsync(userDto.Email);

                if (existingUser != null)
                {
                    throw new CustomException(ErrorMessage.UserAlreadyExists, StatusCode.BadRequest);
                }

                var user = new User
                {
                    Email = userDto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    PhoneNumber = userDto.PhoneNumber,
                    Role = userDto.Role
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return _mapper.Map<RegisterResponseDto>(user);
            }
            else
            {
                throw new CustomException(ErrorMessage.RegisterOnlyUserRole, StatusCode.BadRequest);
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var existingUser = await GetByIdOrThrowAsync(id);
            existingUser.IsActive = false;

            _context.Users.Update(existingUser);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                throw new CustomException(ErrorMessage.UserUpdateFailed, StatusCode.InternalServerError);

            }
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var list = await _context.Users.Where(x => x.IsActive == true).ToListAsync();

            return _mapper.Map<List<UserResponseDto>>(list);
        }

        public async Task<User> GetByEmailOrThrowAsync(string email)
        {
            var user = await GetByEmailAsync(email) ??
                throw new CustomException(ErrorMessage.UserNotFound, StatusCode.NotFound);

            return user;
        }

        public Task<User> GetByEmailAsync(string email)
        {
            var user = _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task<User> GetByIdOrThrowAsync(Guid id)
        {
            var user = await GetByIdAsync(id) ??
                throw new CustomException(ErrorMessage.UserNotFound, StatusCode.NotFound);

            return user;
        }

        public async Task<UserResponseDto> UpdateUserAsync(Guid id, UserRequestDto userDto)
        {
            var existingUserEmail = await GetByEmailAsync(userDto.Email);

            if (existingUserEmail != null && existingUserEmail.Id != id)
            {
                throw new CustomException(ErrorMessage.UserAlreadyExists, StatusCode.BadRequest);
            }

            var user = await GetByIdOrThrowAsync(id);

            _mapper.Map(userDto, user);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return _mapper.Map<UserResponseDto>(user);
            }
            else
            {
                throw new CustomException(ErrorMessage.UserUpdateFailed, StatusCode.InternalServerError);

            }
        }
    }
}
