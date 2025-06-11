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
        private readonly ValidationService _validationsService = new ValidationService();

        public StudentService(DBContexto connection, IMapper mapper)
        {
            _context = connection;
            _mapper = mapper;
        }
        public async Task<PagedResult<StudentResponseDTO>> GetAllAsync(int currentPage = 1, int pageSize = 10)
        {
            var query = _context.Students.AsQueryable();

            int totalItems = await query.CountAsync();

            var students = await query.Where(Student => Student.IsActive)
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
            var womenList = await GetAllByGender(Gender.Female);

            return womenList;
        }


        public async Task<List<StudentResponseDTO>> GetAllMen()
        {
            var menList = await GetAllByGender(Gender.Male);

            return menList;
        }

        public async Task<List<StudentResponseDTO>> GetAllByGender(Gender gender)
        {
            var students = await _context.Students.Where(s => s.Gender == gender && s.IsActive == true).ToListAsync();

            return _mapper.Map<List<StudentResponseDTO>>(students);

        }

        public async Task<StudentResponseDTO> CreateAsync(StudentRequestDTO student)
        {
            _validationsService.ValidateBirthDate(student.DateOfBirth);

            var existingStudent = await GetByNationalIdAsync(student.NationalId);

            if (existingStudent != null) 
                throw new CustomException(ErrorMessage.StudentAlreadyExists, StatusCode.BadRequest);
        

            var newStudent = _mapper.Map<Student>(student);

            await _context.Students.AddAsync(newStudent);
            await _context.SaveChangesAsync();

            return  _mapper.Map<StudentResponseDTO>(newStudent);
        }

        public async Task<Student> GetByNationalIdAsync(string nationalId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => 
            x.NationalId == nationalId && x.IsActive == true);

            return student;
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => 
            s.Id == id && s.IsActive == true);

            return student;

        }

        public async Task<Student> GetByNationalIdOrThrow(string nationalId)
        {
            var student = await GetByNationalIdAsync(nationalId) ??
            throw new CustomException(ErrorMessage.StudentNotFound, StatusCode.NotFound); ;
            
            return student;
        }

        public async Task<Student> GetByIdOrThrow(Guid id)
        {
            var student = await GetByIdAsync(id) ?? 
            throw new CustomException(ErrorMessage.StudentNotFound, StatusCode.NotFound);

            return student;
        }

        public async Task<StudentResponseDTO> UpdateAsync(Guid id, StudentRequestDTO student)
        {
            _validationsService.ValidateBirthDate(student.DateOfBirth);

            Student studentToUpdate = await GetByIdOrThrow(id);

            var existingNationalId = await GetByNationalIdAsync(student.NationalId);

            if (existingNationalId != null && studentToUpdate.Id != existingNationalId.Id)
            {
                throw new CustomException(ErrorMessage.StudentAlreadyExists, StatusCode.BadRequest);
            }
            _mapper.Map(student, studentToUpdate);

            await _context.SaveChangesAsync();

            return _mapper.Map<StudentResponseDTO>(studentToUpdate);

        }

        public async Task<StudentResponseDTO> DeleteAsync(Guid id)
        {
            Student existingStudent = await GetByIdOrThrow(id);

                existingStudent.IsActive = false;

                await _context.SaveChangesAsync();
                return _mapper.Map<StudentResponseDTO>(existingStudent);

        }

    }
}