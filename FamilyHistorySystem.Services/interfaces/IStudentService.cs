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
        Task<List<Estudiante>> GetAllAsync();
        Task<List<Estudiante>> GetAllMen();
        Task<List<Estudiante>> GetAllWomen();
        Task<Estudiante> CreateAsync(EstudianteDTO estudiante);
        Task<Estudiante> UpdateAsync(int id, EstudianteDTO estudiante);
        Task<Estudiante> DeleteAsync(string cedula);
        Task<Estudiante> GetByIdAsync (int id);
        Task<Estudiante> GetByCedulaAsync(string cedula);
        Task<Estudiante> GetByIdOrThrow(int id);
        Task<Estudiante> GetByCedulaOrThrow(string cedula);


    }
}
