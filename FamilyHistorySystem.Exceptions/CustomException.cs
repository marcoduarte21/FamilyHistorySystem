using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Exceptions

{
    public class CustomException : Exception
    {

        public int StatusCode { get; set; }
        public CustomException(string message, int statusCode): base(message) 
        {
            StatusCode = statusCode;
        }

    }
}
