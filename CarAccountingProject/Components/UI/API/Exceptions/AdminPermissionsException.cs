using System;

namespace API.Exceptions
{
    public class AdminPermissionsException: Exception
    {
        public AdminPermissionsException(): base() {}

        public AdminPermissionsException(string? mes): base(mes) {}

        public AdminPermissionsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}