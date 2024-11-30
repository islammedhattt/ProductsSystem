using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        string errorMessage;
        int? errorCode;
        public NotFoundException(string message,int? code = 404) : base(message)
        {
            errorMessage = message;
            if (code != null)
                errorCode = code.Value;
        }
    }
}
