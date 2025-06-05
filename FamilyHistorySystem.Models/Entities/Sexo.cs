using System.ComponentModel.DataAnnotations;

namespace FamilyHistorySystem.Models.Entities
{
    public enum Sexo
    {
        [Required(ErrorMessage = "El campo Sexo es requerido.")]
        MASCULINO = 1, FEMENINO = 2,
    }
}