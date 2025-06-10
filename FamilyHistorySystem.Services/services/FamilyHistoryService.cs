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

        public async Task<List<StudentResponseDTO>> GetChildren(string cedula)
        {
            Estudiante parent = await _studentService.GetByCedulaOrThrow(cedula);

            var children = await _context.Estudiantes.Where(e =>
            e.CedulaPadre == parent.Cedula
            || e.CedulaMadre == parent.Cedula).ToListAsync();

            if (!children.Any())
            {
                throw new CustomException(ErrorMessage.NoChildrenFound, StatusCode.NotFound);
            }
            else
            {
                return _mapper.Map<List<StudentResponseDTO>>(children);
            }
        }

        public async Task<List<StudentResponseDTO>> GetCousins(string cedula)
        {
            Estudiante estudiante = await _studentService.GetByCedulaOrThrow(cedula);
            var uncles = await GetUncles(cedula);
            var cedulasUncles = uncles.Select(u => u.Cedula).ToList();

            var cousins = await _context.Estudiantes
                .Where(e => (cedulasUncles.Contains(e.CedulaPadre)
                || cedulasUncles.Contains(e.CedulaMadre))).ToListAsync();

            if (!cousins.Any())
            {
                throw new CustomException(ErrorMessage.NoCousinsFound, StatusCode.NotFound);
            }

            return _mapper.Map<List<StudentResponseDTO>>(cousins);
        }

        public async Task<List<StudentResponseDTO>> GetGrandParents(string cedula)
        {
            Estudiante student;
            student = await _studentService.GetByCedulaOrThrow(cedula);

            var parents = await GetParents(cedula);

            var grandparentsID = parents.SelectMany(p =>
            new[] { p.CedulaMadre, p.CedulaPadre }).Distinct().ToList();

            var grandparents = await _context.Estudiantes
                .Where(e => grandparentsID.Contains(e.Cedula)).ToListAsync();

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
            Estudiante student;
            student = await _studentService.GetByCedulaOrThrow(cedula);
            var parents = await _context.Estudiantes
                .Where(e =>
                e.Cedula == student.CedulaPadre
                || e.Cedula == student.CedulaMadre).ToListAsync();

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
            Estudiante student;
            student = await _studentService.GetByCedulaOrThrow(cedula);

            string fatherID = student.CedulaPadre;
            string motherID = student.CedulaMadre;


            var siblings = await _context.Estudiantes.Where(e =>
            (e.CedulaPadre == fatherID || e.CedulaMadre == motherID)
                && e.Cedula != student.Cedula).ToListAsync();

            if (!siblings.Any())
            {
                throw new CustomException(ErrorMessage.NoSiblingsFound, StatusCode.NotFound);
            }

            return _mapper.Map<List<StudentResponseDTO>>(siblings);
        }

        public async Task<List<StudentResponseDTO>> GetUncles(string cedula)
        {
            Estudiante student;
            student = await _studentService.GetByCedulaOrThrow(cedula);

            var parents = await GetGrandParents(student.Cedula);
            var parentsID = parents.Select(p => p.Cedula).ToList();

            var uncles = await _context.Estudiantes
                .Where(e => (parentsID.Contains(e.CedulaPadre)
                || parentsID.Contains(e.CedulaMadre))
                && (e.Cedula != student.CedulaPadre
                && e.Cedula != student.CedulaMadre)).ToListAsync();

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
