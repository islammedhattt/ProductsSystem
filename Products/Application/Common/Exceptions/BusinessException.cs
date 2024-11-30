using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class BusinessException : Exception
    {
        string errorMessage;
        int? errorCode;
        public BusinessException(string message, int? code) : base(message)
        {
            errorMessage = message;
            if(code != null)
                errorCode = code.Value;
            else 
                errorCode = -1;
        }
        public BusinessException() : base() {
            errorMessage = "BusinessException";
            errorCode = -1;
        }
    }
}
