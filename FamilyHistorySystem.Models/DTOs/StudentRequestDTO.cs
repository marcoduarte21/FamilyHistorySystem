using FamilyHistorySystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Models.DTOs
{
    public class StudentRequestDTO
    {

        [Required]
        public string NationalId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string MotherNationalId { get; set; }

        [Required]
        public string FatherNationalId { get; set; }
    }
}
