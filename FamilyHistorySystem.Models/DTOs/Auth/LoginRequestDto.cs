using FamilyHistorySystem.Utils.constants;
using FamilyHistorySystem.Utils.constants.messages.ErrorMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Models.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        [RegularExpression(RegexValidation.EmailPattern,
            ErrorMessage = ErrorMessage.InvalidEmail)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(RegexValidation.PasswordPattern,
            ErrorMessage = ErrorMessage.InvalidPassword)]
        public string Password { get; set; }
    }
}
