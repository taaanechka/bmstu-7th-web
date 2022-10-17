using System;

namespace BL
{
    public class AuthorizationFailException: Exception
    {
        public AuthorizationFailException(): base() {}

        public AuthorizationFailException(string? mes): base(mes) {}

        public AuthorizationFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}