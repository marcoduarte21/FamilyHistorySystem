﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Models.DTOs.Auth
{
    public class RefreshTokenRequestDto
    {

        [Required]
        public string RefreshToken { get; set; }
    }
}
