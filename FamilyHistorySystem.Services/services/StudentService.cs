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

namespace FamilyHistorySystem.Services.services
{
    public class StudentService(DBContexto connection) : IStudentService
    {

        private DBContexto Connection = connection;

        public async Task<List<Estudiante>> GetAllWomen()
        {
           var womenList = from estudiante in Connection.Estudiantes
                                           where estudiante.Sexo == Sexo.FEMENINO   
                                           select estudiante;

            return await womenList.ToListAsync();

        }

        public async Task<List<Estudiante>> GetAllAsync()
        {
            
            return await Connection.Estudiantes.ToListAsync();
        }

        public async Task<List<Estudiante>> GetAllMen()
        {
            var menList =from estudiante in Connection.Estudiantes
                                            where estudiante.Sexo == Sexo.MASCULINO
                                            select estudiante;

            return await menList.ToListAsync();

        }

        public async Task<Estudiante> CreateAsync(EstudianteDTO estudiante)
        {
            Estudiante existingStudent = await GetByCedulaAsync(estudiante.Cedula);

            if (existingStudent != null)
            {
                throw new CustomException("student already exists.", 400);
            }

            Estudiante newStudent = new()
            {
                Cedula = estudiante.Cedula,
                CedulaMadre = estudiante.CedulaMadre,
                CedulaPadre = estudiante.CedulaPadre,
                FechaDeNacimiento = estudiante.FechaDeNacimiento,
                Sexo = estudiante.Sexo,
                Nombre = estudiante.Nombre,
                PrimerApellido = estudiante.PrimerApellido,
                SegundoApellido = estudiante.SegundoApellido
            };

            await Connection.Estudiantes.AddAsync(newStudent);
                await Connection.SaveChangesAsync();

                return newStudent;
        }

        public async Task<Estudiante> GetByCedulaAsync(string cedula)
        {
            var estudiante = await Connection.Estudiantes.FirstOrDefaultAsync(x => x.Cedula == cedula);

            return estudiante;
        }

        public async Task<Estudiante> GetByIdAsync(int id)
        {
            var estudiante = await Connection.Estudiantes.FindAsync(id);

            return estudiante;

        }

        public async Task<Estudiante> UpdateAsync(int id, EstudianteDTO estudiante)
        {
            Estudiante existingStudent = await GetByCedulaAsync(estudiante.Cedula);

            if(existingStudent.Equals(null)) throw new CustomException("student not found to update", 404);

            if (!existingStudent.Equals(null) && !existingStudent.Id.Equals(id))
            {
                throw new CustomException("Student cedula already exists", 400);
            }

                existingStudent.Cedula = estudiante.Cedula;
                existingStudent.Nombre = estudiante.Nombre;
                existingStudent.PrimerApellido = estudiante.PrimerApellido;
                existingStudent.SegundoApellido = estudiante.SegundoApellido;
                existingStudent.Sexo = estudiante.Sexo;
                existingStudent.FechaDeNacimiento = estudiante.FechaDeNacimiento;
                existingStudent.CedulaPadre = estudiante.CedulaPadre;
                existingStudent.CedulaMadre = estudiante.CedulaMadre;
                Connection.Estudiantes.Update(existingStudent);
                await Connection.SaveChangesAsync();
                
                return existingStudent;
                
        }

        public async Task<Estudiante> DeleteAsync(string cedula)
        {
            Estudiante existingStudent = await GetByCedulaAsync(cedula);

            if(existingStudent == null)
            {
                throw new CustomException("student not found to delete", 404);
            }

            Connection.Estudiantes.Remove(existingStudent);
            await Connection.SaveChangesAsync();

            return existingStudent;

        }
    }
}