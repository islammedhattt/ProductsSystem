using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public string[] errorMessage;
        public int? errorCode;
        public ValidationException(string[] message, int? code) : base()
        {
            errorMessage = message;
            if (code != null)
                errorCode = code.Value;
            else
                errorCode = -101;

        }
    }
}
