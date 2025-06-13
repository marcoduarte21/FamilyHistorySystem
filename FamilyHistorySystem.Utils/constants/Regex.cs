using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Utils.constants
{
    public class RegexValidation
    {
        public const string only8Digits = @"^\d{8}$";
        public const string onlyLettersAndSpaces = @"^[a-zA-Z\s]+$";
        public const int Length8 = 8;
        public const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$";
        public const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    }
}
