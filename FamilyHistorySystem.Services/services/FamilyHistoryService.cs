using NuGet.Protocol.Plugins;
using FamilyHistorySystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using FamilyHistorySystem.Exceptions;
using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Services.interfaces;
using System.Collections;
using AutoMapper;
using FamilyHistorySystem.Utils.constants.messages.ErrorMessage;
using FamilyHistorySystem.Utils.constants.Response;
using FamilyHistorySystem.Models.DTOs;

namespace FamilyHistorySystem.Services.services
{
    public class FamilyHistoryService : IFamilyHistory
    {
        private readonly IStudentService _studentService;
        private readonly DBContexto _context;
        private readonly IMapper _mapper;

        public FamilyHistoryService(IStudentService studentService, DBContexto context, IMapper mapper)
        {
            _studentService = studentService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StudentResponseDTO>> GetChildren(string nationalId)
        {
            Student parent = await _studentService.GetByNationalIdOrThrow(nationalId);

            var children = await _context.Students.Where(e =>
            e.FatherNationalId == parent.NationalId
            || e.MotherNationalId == parent.NationalId).ToListAsync();

            if (!children.Any())
            {
                throw new CustomException(ErrorMessage.NoChildrenFound, StatusCode.NotFound);
            }
            else
            {
                return _mapper.Map<List<StudentResponseDTO>>(children);
            }
        }

        public async Task<List<StudentResponseDTO>> GetCousins(string nationalId)
        {
            Student estudiante = await _studentService.GetByNationalIdOrThrow(nationalId);
            var uncles = await GetUncles(nationalId);
            var cedulasUncles = uncles.Select(u => u.NationalId).ToList();

            var cousins = await _context.Students
                .Where(e => (cedulasUncles.Contains(e.FatherNationalId)
                || cedulasUncles.Contains(e.MotherNationalId))).ToListAsync();

            if (!cousins.Any())
            {
                throw new CustomException(ErrorMessage.NoCousinsFound, StatusCode.NotFound);
            }

            return _mapper.Map<List<StudentResponseDTO>>(cousins);
        }

        public async Task<List<StudentResponseDTO>> GetGrandParents(string nationalId)
        {
            Student student = await _studentService.GetByNationalIdOrThrow(nationalId);
             
            var parents = await GetParents(nationalId);

            var grandparentsID = parents.SelectMany(p =>
            new[] { p.MotherNationalId, p.FatherNationalId }).Distinct().ToList();

            var grandparents = await _context.Students
                .Where(e => grandparentsID.Contains(e.NationalId)).ToListAsync();

            if (!grandparents.Any())
            {
                throw new CustomException(ErrorMessage.NoGrandParentsFound, StatusCode.NotFound);
            }
            else
            {
                return _mapper.Map<List<StudentResponseDTO>>(grandparents);
            }
        }

        public async Task<List<StudentResponseDTO>> GetParents(string cedula)
        {
            Student student = await _studentService.GetByNationalIdOrThrow(cedula);
            var parents = await _context.Students
                .Where(e =>
                e.NationalId == student.FatherNationalId
                || e.NationalId == student.MotherNationalId).ToListAsync();

            if (parents.Any())
            {
                return _mapper.Map<List<StudentResponseDTO>>(parents);
            }
            else
            {
                throw new CustomException(ErrorMessage.NoParentsFound, StatusCode.NotFound);
            }
        }

        public async Task<List<StudentResponseDTO>> GetSiblings(string cedula)
        {
            Student student = await _studentService.GetByNationalIdOrThrow(cedula);

            string fatherID = student.FatherNationalId;
            string motherID = student.MotherNationalId;


            var siblings = await _context.Students.Where(e =>
            (e.FatherNationalId == fatherID || e.MotherNationalId == motherID)
                && e.NationalId != student.NationalId).ToListAsync();

            if (!siblings.Any())
            {
                throw new CustomException(ErrorMessage.NoSiblingsFound, StatusCode.NotFound);
            }

            return _mapper.Map<List<StudentResponseDTO>>(siblings);
        }

        public async Task<List<StudentResponseDTO>> GetUncles(string nationalId)
        {
            Student student = await _studentService.GetByNationalIdAsync(nationalId);

            var parents = await GetGrandParents(student.NationalId);
            var parentsID = parents.Select(p => p.NationalId).ToList();

            var uncles = await _context.Students
                .Where(e => (parentsID.Contains(e.FatherNationalId)
                || parentsID.Contains(e.MotherNationalId))
                && (e.NationalId != student.FatherNationalId
                && e.NationalId != student.MotherNationalId)).ToListAsync();

            if (!uncles.Any())
            {
                throw new CustomException(ErrorMessage.NoUnclesFound, StatusCode.NotFound);
            }
            else
            {
                return _mapper.Map<List<StudentResponseDTO>>(uncles);
            }
        }
    }
}
