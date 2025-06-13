using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Utils.constants.messages.ErrorMessage
{
    public class ErrorMessage
    {
        public const string StudentNotFound = "Student not found";
        public const string StudentAlreadyExists = "Student already exists";
        public const string UserNotFound = "User not found";
        public const string UserAlreadyExists = "User already exists";
        public const string StudentNotDeleted = "Student could not be deleted";
        public const string UnauthorizedAccess = "Unauthorized access";
        public const string Forbidden = "You do not have permission to perform this action";
        public const string InvalidToken = "Invalid or expired token";
        public const string InvalidCredentials = "Invalid email or password";
        public const string NoParentsFound = "No parents found for this student";
        public const string NoChildrenFound = "No children found for this student";
        public const string NoSiblingsFound = "No siblings found for this student";
        public const string NoGrandParentsFound = "No grandparents found for this student";
        public const string NoUnclesFound = "No uncles found for this student";
        public const string NoCousinsFound = "No cousins found for this student";
        public const string NationalIdMinLength = "National ID must be 8 characters long.";
        public const string OnlyDigitsNationalId = "National ID must be exactly 8 digits.";
        public const string FirstNameOnlyLetters = "First name can only contain letters and spaces.";
        public const string LastNameOnlyLetters = "Last name can only contain letters and spaces.";
        public const string InvalidBirthDate = "Birth date cannot be in the future.";
        public const string BirthDateRequired = "Birth date is required.";
        public const string InvalidPassword = "Password must be at least 8 characters long, " +
            "contain at least one uppercase letter, one lowercase letter, and one number.";
        public const string InvalidEmail = "Invalid email format.";
        public const string PasswordsDoNotMatch = "Passwords do not match.";
        public const string InvalidPhoneNumber = "Phone number must be exactly 8 digits.";
        public const string LogoutFailed = "Logout failed. Please try again later.";
        public const string RegisterOnlyUserRole = "Only user role is allowed for this register section.";
        public const string UserUpdateFailed = "User update failed. Please try again later.";
        public const string ErrorAlreadyLogged = "Sorry you can´t login, User already logged.";
    }
}
