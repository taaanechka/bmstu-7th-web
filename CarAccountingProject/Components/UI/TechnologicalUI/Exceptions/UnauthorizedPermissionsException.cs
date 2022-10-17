using System;

namespace TechnologicalUI.Exceptions
{
    public class UnauthorizedPermissionsException: Exception
    {
        public UnauthorizedPermissionsException(): base() {}

        public UnauthorizedPermissionsException(string? mes): base(mes) {}

        public UnauthorizedPermissionsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}