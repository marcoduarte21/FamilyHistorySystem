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

namespace FamilyHistorySystem.Services.services
{
    public class FamilyHistoryService(DBContexto connection, IMapper mapper) : IFamilyHistory
    {
        private readonly DBContexto Connection = connection;
        private readonly StudentService StudentService = new StudentService(connection, mapper);

        public async Task<List<Estudiante>> GetChildren(string cedula)
        {
            Estudiante estudiantePadre = await StudentService.GetByCedulaAsync(cedula) ?? 
                throw new CustomException("Student not Found", 404);

            var children = from estudiante in Connection.Estudiantes
                           where (estudiante.CedulaPadre == estudiantePadre.Cedula) ||
                           (estudiante.CedulaMadre == estudiantePadre.Cedula)
                           select estudiante;

            if (!children.Any())
            {
                throw new CustomException("Student doesn't have children registered in this school.", 404);
            }
            else
            {
                return await children.ToListAsync();
            }
        }

        public async Task<List<Estudiante>> GetCousins(string cedula)
        {
            var listaDeTios = await GetUncles(cedula);
            var lista = new List<Estudiante>();

            if (listaDeTios != null)
            {
                foreach (var tio in listaDeTios)
                {
                    foreach (var primo in Connection.Estudiantes)
                    {
                        if (tio.Cedula == primo.CedulaPadre || tio.Cedula == primo.CedulaMadre)
                        {
                            lista.Add(primo);
                        }
                    }
                }

                return lista.ToList();
            }

            return null;
        }

        public async Task<List<Estudiante>> GetGrandParents(string cedula)
        {
            Estudiante estudianteHijo;
            estudianteHijo = await StudentService.GetByCedulaAsync(cedula);
            List<Estudiante> parentsList = await GetParents(cedula);
            List<Estudiante> grandParents = [];

            if (estudianteHijo == null)
            {
                throw new CustomException("student not found", 404);
            }

            foreach (var parent in parentsList)
            {

                if ((parent.CedulaPadre != null && parent.CedulaMadre != null)
                    || (parent.CedulaPadre != null) || (parent.CedulaMadre != null))
                {
                    var grandMother = await StudentService.GetByCedulaAsync(parent.CedulaMadre);
                    var grandFather = await StudentService.GetByCedulaAsync(parent.CedulaPadre);
                    if (grandMother != null)
                    {
                        grandParents.Add(grandMother);
                    }

                    if (grandFather != null)
                    {
                        grandParents.Add(grandFather);
                    }
                }
            }

            if (grandParents.Count > 0)
            {
                return grandParents;
            }
            else
            {
                throw new CustomException("Student doesn't have grandparents registered in this school.", 404);
            }

        }

        public async Task<List<Estudiante>> GetParents(string cedula)
        {
            Estudiante estudianteHijo;
            estudianteHijo = await StudentService.GetByCedulaAsync(cedula);

            if (estudianteHijo == null)
            {
                throw new CustomException("Student not found.", 404);
            }

            var InformacionDelPadre = from estudiante in Connection.Estudiantes
                                      where estudiante.Cedula == estudianteHijo.CedulaPadre
                                      || estudiante.Cedula == estudianteHijo.CedulaMadre
                                      select estudiante;

            if (InformacionDelPadre.Count() > 0)
            {
                return await InformacionDelPadre.ToListAsync();
            }
            else
            {
                throw new CustomException("Student doesn't have parents registered in this school.", 404);
            }
        }

        public async Task<List<Estudiante>> GetSiblings(string cedula)
        {
            Estudiante estudianteAConsultar;
            estudianteAConsultar = await StudentService.GetByCedulaAsync(cedula);
            if(estudianteAConsultar == null)
            {
                throw new CustomException("Student not found.", 404);
            }
            string cedulaDelPadre = estudianteAConsultar.CedulaPadre;
            string cedulaDeLaMadre = estudianteAConsultar.CedulaMadre;

            var listaDeHermanos = from estudiante in Connection.Estudiantes
                                  where (estudiante.CedulaPadre == cedulaDelPadre && estudiante.CedulaMadre == cedulaDeLaMadre
                                  && estudiante.Cedula != cedula) || (estudiante.CedulaPadre == cedulaDelPadre && estudiante.Cedula != cedula)
                                  || (estudiante.CedulaMadre == cedulaDeLaMadre && estudiante.Cedula != cedula)
                                  select estudiante;

            if(listaDeHermanos.Count() == 0)
            {
                throw new CustomException("Student doesn't have siblings registered in this school.", 404);
            }   

            return await listaDeHermanos.ToListAsync();
        }

        public async Task<List<Estudiante>> GetUncles(string cedula)
        {
            Estudiante student;
            student = await StudentService.GetByCedulaAsync(cedula);
            if(student == null)
            {
                throw new CustomException("Student not found.", 404);
            }

            List<Estudiante> uncles = [];
            List<Estudiante> parents = await GetGrandParents(student.Cedula);
            List<Estudiante> children = [];

            foreach (Estudiante parent in parents)
            {
                if (parent != null) children = await GetChildren(parent.Cedula);
                else continue;
                foreach (Estudiante child in children)
                {
                    if (child.Cedula != student.CedulaPadre && child.Cedula != student.CedulaMadre)
                    {
                        uncles.Add(child);
                    }
                }
            }

            if(uncles.Count == 0)
            {
                throw new CustomException("Student doesn't have uncles registered in this school.", 404);
            }
            List<Estudiante> noDuplicates = new HashSet<Estudiante>(uncles).ToList();
            return noDuplicates;
            
        }
    }
}
