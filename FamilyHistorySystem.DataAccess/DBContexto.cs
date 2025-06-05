using FamilyHistorySystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FamilyHistorySystem.DataAccess
{
    public class DBContexto(DbContextOptions<DBContexto> options) : DbContext(options)
    {
        public DbSet<Estudiante> Estudiantes { get; set;}


    }


}