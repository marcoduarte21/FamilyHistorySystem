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

        public async Task<List<Estudiante>> GetAllWomen()
        {
            var womenList = from estudiante in _context.Estudiantes
                            where estudiante.Sexo == Sexo.FEMENINO
                            select estudiante;

            return await womenList.ToListAsync();

        }

        public async Task<List<Estudiante>> GetAllAsync()
        {

            return await _context.Estudiantes.ToListAsync();
        }

        public async Task<List<Estudiante>> GetAllMen()
        {
            var menList = from estudiante in _context.Estudiantes
                          where estudiante.Sexo == Sexo.MASCULINO
                          select estudiante;

            return await menList.ToListAsync();

        }

        public async Task<Estudiante> CreateAsync(EstudianteDTO estudiante)
        {
            Estudiante existingStudent = await GetByCedulaAsync(estudiante.Cedula);

            if (existingStudent != null)
            {
                throw new CustomException(ErrorMessage.StudentAlreadyExists, StatusCode.BadRequest);
            }

            var newStudent = _mapper.Map<Estudiante>(estudiante);

            await _context.Estudiantes.AddAsync(newStudent);
            await _context.SaveChangesAsync();

            return newStudent;
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

        public async Task<Estudiante> UpdateAsync(int id, EstudianteDTO estudiante)
        {
            Estudiante studentToUpdate = await GetByIdOrThrow(id);

            Estudiante existingStudent = await GetByCedulaAsync(estudiante.Cedula);

            if (existingStudent != null && studentToUpdate.Id != existingStudent.Id)
            {
                throw new CustomException(ErrorMessage.StudentAlreadyExists, StatusCode.BadRequest);
            }

            _mapper.Map(estudiante, studentToUpdate);

            _context.Estudiantes.Update(studentToUpdate);
            await _context.SaveChangesAsync();

            return studentToUpdate;

        }

        public async Task<Estudiante> DeleteAsync(string cedula)
        {
            Estudiante existingStudent = await GetByCedulaAsync(cedula);

            _context.Estudiantes.Remove(existingStudent);
            await _context.SaveChangesAsync();

            return existingStudent;

        }

    }
}