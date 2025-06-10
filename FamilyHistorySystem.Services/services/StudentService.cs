using FamilyHistorySystem.Exceptions;
using FamilyHistorySystem.DataAccess;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Services.interfaces;
using FamilyHistorySystem.Models.DTOs;
using AutoMapper;
using FamilyHistorySystem.Utils.constants.messages.ErrorMessage;
using FamilyHistorySystem.Utils.constants.Response;

namespace FamilyHistorySystem.Services.services
{
    public class StudentService : IStudentService
    {

        private DBContexto _context;
        private readonly IMapper _mapper;

        public StudentService(DBContexto connection, IMapper mapper)
        {
            _context = connection;
            _mapper = mapper;
        }
        public async Task<PagedResult<StudentResponseDTO>> GetAllAsync(int currentPage = 1, int pageSize = 10)
        {
            var query = _context.Estudiantes.AsQueryable();

            int totalItems = await query.CountAsync();

            var students = await query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responseList = _mapper.Map<List<StudentResponseDTO>>(students);

            return new PagedResult<StudentResponseDTO>
            {
                Items = responseList,
                TotalItems = totalItems,
                PageSize = pageSize,
                CurrentPage = currentPage,
            };

        }

        public async Task<List<StudentResponseDTO>> GetAllWomen()
        {
            var womenList = await GetAllByGender(Sexo.FEMENINO);

            return womenList;
        }


        public async Task<List<StudentResponseDTO>> GetAllMen()
        {
            var menList = await GetAllByGender(Sexo.MASCULINO);

            return menList;
        }

        public async Task<List<StudentResponseDTO>> GetAllByGender(Sexo sexo)
        {
            var students = await _context.Estudiantes.Where(s => s.Sexo == sexo).ToListAsync();

            return _mapper.Map<List<StudentResponseDTO>>(students);

        }

        public async Task<StudentResponseDTO> CreateAsync(StudentRequestDTO estudiante)
        {
            var existingStudent = await GetByCedulaAsync(estudiante.Cedula);

            if (existingStudent != null) 
                throw new CustomException(ErrorMessage.StudentAlreadyExists, StatusCode.BadRequest);
        

            var newStudent = _mapper.Map<Estudiante>(estudiante);

            await _context.Estudiantes.AddAsync(newStudent);
            await _context.SaveChangesAsync();

            return  _mapper.Map<StudentResponseDTO>(newStudent);
        }

        public async Task<Estudiante> GetByCedulaAsync(string cedula)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Cedula == cedula);

            return estudiante;
        }

        public async Task<Estudiante> GetByIdAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);

            return estudiante;

        }

        public async Task<Estudiante> GetByCedulaOrThrow(string cedula)
        {
            var estudiante = await GetByCedulaAsync(cedula) ??
            throw new CustomException(ErrorMessage.StudentNotFound, StatusCode.NotFound); ;
            
            return estudiante;
        }

        public async Task<Estudiante> GetByIdOrThrow(int id)
        {
            var estudiante = await GetByIdAsync(id) ?? 
            throw new CustomException(ErrorMessage.StudentNotFound, StatusCode.NotFound);

            return estudiante;
        }

        public async Task<StudentResponseDTO> UpdateAsync(int id, StudentRequestDTO estudiante)
        {
            Estudiante studentToUpdate = await GetByIdOrThrow(id);

            var existingCedula = await GetByCedulaAsync(estudiante.Cedula);

            if (existingCedula != null && studentToUpdate.Id != existingCedula.Id)
            {
                throw new CustomException(ErrorMessage.StudentAlreadyExists, StatusCode.BadRequest);
            }
            _mapper.Map(estudiante, studentToUpdate);

            await _context.SaveChangesAsync();

            return _mapper.Map<StudentResponseDTO>(studentToUpdate);

        }

        public async Task<StudentResponseDTO> DeleteAsync(string cedula)
        {
            var existingStudent = _mapper.Map<Estudiante>(await GetByCedulaOrThrow(cedula));

            _context.Estudiantes.Remove(existingStudent);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudentResponseDTO>(existingStudent);

        }

    }
}