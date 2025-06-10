using System.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using FamilyHistorySystem.Exceptions;

namespace FamilyHistorySystem.Models.Entities
{
    public class Estudiante
    {
        [Key]
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