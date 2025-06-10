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

        public async Task<List<StudentResponseDTO>> GetAllWomen()
        {
            var womenList = await _context.Estudiantes.Where(s => s.Sexo == Sexo.FEMENINO).ToListAsync();

            return _mapper.Map<List<StudentResponseDTO>>(womenList);
        }

        public async Task<List<StudentResponseDTO>> GetAllAsync()
        {

            return _mapper.Map<List<StudentResponseDTO>>(await _context.Estudiantes.ToListAsync());
        }

        public async Task<List<StudentResponseDTO>> GetAllMen()
        {
            var menList = await _context.Estudiantes.Where(s => s.Sexo == Sexo.MASCULINO).ToListAsync();

            return _mapper.Map<List<StudentResponseDTO>>(menList);

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