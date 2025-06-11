using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Services.interfaces
{
    public interface IValidationsService
    {
        public void ValidateBirthDate(DateTime? birthDate);
    }
}
