using FamilyHistorySystem.DataAccess;
using FamilyHistorySystem.Services.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Models.DTOs;

namespace FamilyHistorySystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController(DBContexto context) : ControllerBase
    {
        private DBContexto _context = context;
        StudentService StudentService = new(context);

        [HttpGet("getStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await StudentService.GetAllAsync();
            return Ok(new { message = "students found successfully", students });
        }

        [HttpGet("getStudentById/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await StudentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with id {id} not found.");
            }
            return Ok(new { message = "student found successfully", student });
        }

        [HttpGet("getStudentByCedula/{cedula}")]
        public async Task<IActionResult> GetStudentByCedula(string cedula)
        {
            var student  = await StudentService.GetByCedulaAsync(cedula);
            if(student == null)
            {
                return NotFound($"Student with cedula {cedula} not found.");
            }
            return Ok( new { message = "student found successfully", student });

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent([FromBody] EstudianteDTO student)
        {
                if (ModelState.IsValid)
                {
                var newStudent = await StudentService.CreateAsync(student);
                return CreatedAtAction(nameof(GetStudent), new { newStudent.Id }, new { message = "student created sucessfully", newStudent });
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
                var student = await StudentService.UpdateAsync(id, estudiante);
                return Ok(new { message = "Student updated successfully", student });
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete("delete/{cedula}")]
        public async Task<IActionResult> DeleteStudent(string cedula)
        {
            var student = await StudentService.DeleteAsync(cedula);

            if(student != null)
            {
                return Ok(new { message = "Student deleted successfully", student });
            }
            else
            {
                return BadRequest(new { message = "Something went wrong deleting this student" });
            }
        }

    }


}
