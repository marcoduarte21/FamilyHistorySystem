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
        Task<List<StudentResponseDTO>> GetChildren(string cedula);
        Task<List<StudentResponseDTO>> GetParents(string cedula);
        Task<List<StudentResponseDTO>> GetSiblings(string cedula);
        Task<List<StudentResponseDTO>> GetGrandParents(string cedula);
        Task<List<StudentResponseDTO>> GetUncles(string cedula);
        Task<List<StudentResponseDTO>> GetCousins(string cedula);
    }
}
