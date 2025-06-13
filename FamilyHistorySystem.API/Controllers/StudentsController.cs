using FamilyHistorySystem.DataAccess;
using FamilyHistorySystem.Services.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Models.DTOs;
using AutoMapper;
using FamilyHistorySystem.Services.interfaces;
using FamilyHistorySystem.Utils.constants.Response;
using FamilyHistorySystem.Utils.constants.messages.successMessage;
using FamilyHistorySystem.Utils.constants.messages.ErrorMessage;
using Microsoft.AspNetCore.Authorization;

namespace FamilyHistorySystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentsController(IStudentService studentService, IMapper mapper) {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {
            var students = await _studentService.GetAllAsync(currentPage, pageSize);
            return Ok(new ApiResponse<PagedResult<StudentResponseDTO>>(SuccessMessage.StudentsFound, students));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _studentService.GetByIdOrThrow(id);
            
            return Ok(new ApiResponse<StudentResponseDTO>(SuccessMessage.StudentFound,
                _mapper.Map<StudentResponseDTO>(student)));
        }

        [HttpGet("GetByNationalId/{nationalId}")]
        public async Task<IActionResult> GetByNationalId(string nationalId)
        {
            var student  = await _studentService.GetByNationalIdOrThrow(nationalId);

            return Ok(new ApiResponse<StudentResponseDTO>(SuccessMessage.StudentFound,
                _mapper.Map<StudentResponseDTO>(student)));

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentRequestDTO student)
        {
                if (ModelState.IsValid)
                {
                var newStudent = await _studentService.CreateAsync(student);
                return CreatedAtAction(nameof(GetById), 
                    new { newStudent.Id }, new ApiResponse<StudentResponseDTO>(SuccessMessage.StudentCreated, newStudent));
            }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StudentRequestDTO studentDto)
        {

            if (ModelState.IsValid)
            {
                var student = await _studentService.UpdateAsync(id, studentDto);
                return Ok(new ApiResponse<StudentResponseDTO>(SuccessMessage.StudentUpdated, student));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await _studentService.DeleteAsync(id);

            if(student != null)
            {
                return Ok(new ApiResponse<StudentResponseDTO>(SuccessMessage.StudentDeleted, student));
            }
            else
            {
                return BadRequest(new ApiResponse<object>(ErrorMessage.StudentNotDeleted, null, false));
            }
        }

    }


}
