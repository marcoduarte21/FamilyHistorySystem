using FamilyHistorySystem.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Models.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public RegisterResponseDto User { get; set; }

    }
}
