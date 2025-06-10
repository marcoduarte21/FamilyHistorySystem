using FamilyHistorySystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Models.DTOs
{
    public class StudentResponseDTO
    {
        public Guid Id { get; set; }

        public string NationalId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string MotherNationalId { get; set; }

        public string FatherNationalId { get; set; }

        public int Age { get; set; }
}
}
