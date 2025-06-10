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
            CreateMap<StudentRequestDTO, Estudiante>();
            CreateMap<Estudiante, StudentResponseDTO>().ForMember(dest => dest.Edad, opt => opt.MapFrom(src =>
            src.FechaDeNacimiento.HasValue
            ? DateTime.Today.Year - src.FechaDeNacimiento.Value.Year
              - (src.FechaDeNacimiento.Value.Date > DateTime.Today.AddYears(-(
                  DateTime.Today.Year - src.FechaDeNacimiento.Value.Year)) ? 1 : 0)
            : 0));
        }
    }
}
