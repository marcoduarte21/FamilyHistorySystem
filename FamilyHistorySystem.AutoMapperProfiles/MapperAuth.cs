using AutoMapper;
using FamilyHistorySystem.Models.DTOs.Auth;
using FamilyHistorySystem.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.AutoMapperProfiles
{
    public class MapperAuth : Profile
    {
        public MapperAuth()
        {
            CreateMap<User, RegisterResponseDto>();
        }
    }
}
