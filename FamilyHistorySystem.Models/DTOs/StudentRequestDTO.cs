using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Utils.constants;
using FamilyHistorySystem.Utils.constants.messages.ErrorMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Models.DTOs
{
    public class StudentRequestDTO
    {

        [Required]
        [StringLength(RegexValidation.Length8, MinimumLength = RegexValidation.Length8, ErrorMessage = ErrorMessage.NationalIdMinLength)]
        [RegularExpression(RegexValidation.only8Digits, ErrorMessage = ErrorMessage.OnlyDigitsNationalId)]
        public string NationalId { get; set; }

        [Required]
        [RegularExpression(RegexValidation.onlyLettersAndSpaces, ErrorMessage = ErrorMessage.FirstNameOnlyLetters)]
        public string FirstName { get; set; }


        [Required]
        [RegularExpression(RegexValidation.onlyLettersAndSpaces, ErrorMessage = ErrorMessage.LastNameOnlyLetters)]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [StringLength(RegexValidation.Length8, MinimumLength = RegexValidation.Length8, ErrorMessage = ErrorMessage.NationalIdMinLength)]
        [RegularExpression(RegexValidation.only8Digits, ErrorMessage = ErrorMessage.OnlyDigitsNationalId)]
        public string MotherNationalId { get; set; }

        [Required]
        [StringLength(RegexValidation.Length8, MinimumLength = RegexValidation.Length8, ErrorMessage = ErrorMessage.NationalIdMinLength)]
        [RegularExpression(RegexValidation.only8Digits, ErrorMessage = ErrorMessage.OnlyDigitsNationalId)]
        public string FatherNationalId { get; set; }
    }
}
