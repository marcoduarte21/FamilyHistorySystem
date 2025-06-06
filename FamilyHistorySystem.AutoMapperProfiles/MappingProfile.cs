using AutoMapper;
using FamilyHistorySystem.Models.DTOs;
using FamilyHistorySystem.Models.Entities;
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
            CreateMap<Estudiante, EstudianteDTO>();
            CreateMap<EstudianteDTO, Estudiante>()
                .ForMember(dest => dest.Edad, opt => opt.Ignore());
        }
    }
}
