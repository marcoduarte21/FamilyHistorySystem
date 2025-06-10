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
        Task<PagedResult<StudentResponseDTO>> GetAllAsync(int currentPage, int pageSize);
        Task<List<StudentResponseDTO>> GetAllByGender(Gender gender);
        Task<List<StudentResponseDTO>> GetAllMen();
        Task<List<StudentResponseDTO>> GetAllWomen();
        Task<StudentResponseDTO> CreateAsync(StudentRequestDTO student);
        Task<StudentResponseDTO> UpdateAsync(Guid id, StudentRequestDTO student);
        Task<StudentResponseDTO> DeleteAsync(Guid id);
        Task<Student> GetByIdAsync (Guid id);
        Task<Student> GetByNationalIdAsync(string nationalId);
        Task<Student> GetByIdOrThrow(Guid id);
        Task<Student> GetByNationalIdOrThrow(string nationalId);
    }
}
