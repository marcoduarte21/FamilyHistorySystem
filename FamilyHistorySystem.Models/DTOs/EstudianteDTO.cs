using FamilyHistorySystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Models.DTOs
{
    public class EstudianteDTO
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public Sexo Sexo { get; set; }
        public DateTime? FechaDeNacimiento { get; set; }
        public string CedulaMadre { get; set; }
        public string CedulaPadre { get; set; }
    }
}
