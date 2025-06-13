using FamilyHistorySystem.Models.Entities.Auth;
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
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        [RegularExpression(RegexValidation.EmailPattern, ErrorMessage = ErrorMessage.InvalidEmail)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(RegexValidation.PasswordPattern, ErrorMessage = ErrorMessage.InvalidPassword)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = ErrorMessage.PasswordsDoNotMatch)]
        public string confirmPassword { get; set; }
        [Required]
        [RegularExpression(RegexValidation.onlyLettersAndSpaces, ErrorMessage = ErrorMessage.FirstNameOnlyLetters)]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(RegexValidation.onlyLettersAndSpaces, ErrorMessage = ErrorMessage.LastNameOnlyLetters)]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(RegexValidation.only8Digits, ErrorMessage = ErrorMessage.InvalidPhoneNumber)]
        public string? PhoneNumber { get; set; }
        [Required]
        public Role Role { get; set; } = Role.user;

    }
}
