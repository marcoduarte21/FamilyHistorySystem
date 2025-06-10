using FamilyHistorySystem.Models.DTOs;
using FamilyHistorySystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Services.interfaces
{
    public interface IFamilyHistory
    {
        Task<List<StudentResponseDTO>> GetChildren(string nationalId);
        Task<List<StudentResponseDTO>> GetParents(string nationalId);
        Task<List<StudentResponseDTO>> GetSiblings(string nationalId);
        Task<List<StudentResponseDTO>> GetGrandParents(string nationalId);
        Task<List<StudentResponseDTO>> GetUncles(string nationalId);
        Task<List<StudentResponseDTO>> GetCousins(string nationalId);
    }
}
