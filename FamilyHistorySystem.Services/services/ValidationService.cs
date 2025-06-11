using FamilyHistorySystem.Exceptions;
using FamilyHistorySystem.Services.interfaces;
using FamilyHistorySystem.Utils.constants.messages.ErrorMessage;
using FamilyHistorySystem.Utils.constants.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Services.services
{
    public class ValidationService : IValidationsService
    {

    public void ValidateBirthDate(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                throw new CustomException(ErrorMessage.BirthDateRequired, StatusCode.BadRequest);
            if (birthDate.Value > DateTime.Today)
                throw new CustomException(ErrorMessage.InvalidBirthDate, StatusCode.BadRequest);
        }
    }
}
    
