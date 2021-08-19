using System;

namespace API.BLL.Utilities.Exceptions
{
    public class ApplicationValidationException : Exception
    {
        public ApplicationValidationException(string message) : base(message)
        {

        }
    }
}