using FamilyHistorySystem.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Services.services
{
    public class AgeService : IAgeService
    {
        public int CalculateAge(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                return 0;

            var today = DateTime.Today;
            var age = today.Year - birthDate.Value.Year;

            if (birthDate.Value.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
