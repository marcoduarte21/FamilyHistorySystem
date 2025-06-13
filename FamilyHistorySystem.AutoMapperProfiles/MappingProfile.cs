using AutoMapper;
using FamilyHistorySystem.Models.DTOs;
using FamilyHistorySystem.Models.DTOs.Auth;
using FamilyHistorySystem.Models.DTOs.user;
using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.AutoMapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<StudentRequestDTO, Student>();
            CreateMap<Student, StudentResponseDTO>().ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
            src.DateOfBirth.HasValue
            ? DateTime.Today.Year - src.DateOfBirth.Value.Year
              - (src.DateOfBirth.Value.Date > DateTime.Today.AddYears(-(
                  DateTime.Today.Year - src.DateOfBirth.Value.Year)) ? 1 : 0)
            : 0));
            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>();
        }
    }
}
