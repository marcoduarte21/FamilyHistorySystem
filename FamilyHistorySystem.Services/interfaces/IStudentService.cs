using FamilyHistorySystem.Models.DTOs;
using FamilyHistorySystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace FamilyHistorySystem.Services.interfaces
{
    public interface IStudentService
    {
        Task<List<StudentResponseDTO>> GetAllAsync();
        Task<List<StudentResponseDTO>> GetAllMen();
        Task<List<StudentResponseDTO>> GetAllWomen();
        Task<StudentResponseDTO> CreateAsync(StudentRequestDTO estudiante);
        Task<StudentResponseDTO> UpdateAsync(int id, StudentRequestDTO estudiante);
        Task<StudentResponseDTO> DeleteAsync(string cedula);
        Task<Estudiante> GetByIdAsync (int id);
        Task<Estudiante> GetByCedulaAsync(string cedula);
        Task<Estudiante> GetByIdOrThrow(int id);
        Task<Estudiante> GetByCedulaOrThrow(string cedula);


    }
}
