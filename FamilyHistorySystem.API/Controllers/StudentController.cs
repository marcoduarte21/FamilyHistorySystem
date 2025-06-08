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

namespace FamilyHistorySystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService) {
            _studentService = studentService;
        }

        [HttpGet("getStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(new ApiResponse<List<Estudiante>>(SuccessMessage.StudentsFound, students));
        }

        [HttpGet("getStudentById/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetByIdOrThrow(id);
            
            return Ok(new ApiResponse<Estudiante>(SuccessMessage.StudentFound, student));
        }

        [HttpGet("getStudentByCedula/{cedula}")]
        public async Task<IActionResult> GetStudentByCedula(string cedula)
        {
            var student  = await _studentService.GetByCedulaOrThrow(cedula);

            return Ok(new ApiResponse<Estudiante>(SuccessMessage.StudentFound, student));

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent([FromBody] EstudianteDTO student)
        {
                if (ModelState.IsValid)
                {
                var newStudent = await _studentService.CreateAsync(student);
                return CreatedAtAction(nameof(GetStudent), 
                    new { newStudent.Id }, new ApiResponse<Estudiante>(SuccessMessage.StudentCreated, newStudent));
            }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> EditStudent(int id, [FromBody] EstudianteDTO estudiante)
        {

            if (ModelState.IsValid)
            {
                var student = await _studentService.UpdateAsync(id, estudiante);
                return Ok(new ApiResponse<Estudiante>(SuccessMessage.StudentUpdated, student));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete("delete/{cedula}")]
        public async Task<IActionResult> DeleteStudent(string cedula)
        {
            var student = await _studentService.DeleteAsync(cedula);

            if(student != null)
            {
                return Ok(new ApiResponse<Estudiante>(SuccessMessage.StudentDeleted, student));
            }
            else
            {
                return BadRequest(new ApiResponse<object>(ErrorMessage.StudentNotDeleted, null, false));
            }
        }

    }


}
