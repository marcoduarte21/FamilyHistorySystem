using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Models.Entities
{
    [Table("Students")]
    public class Student : AuditableEntity
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

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
