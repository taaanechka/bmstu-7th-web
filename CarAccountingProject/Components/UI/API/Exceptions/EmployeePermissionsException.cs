using System;

namespace API.Exceptions
{
    public class EmployeePermissionsException: Exception
    {
        public EmployeePermissionsException(): base() {}

        public EmployeePermissionsException(string? mes): base(mes) {}

        public EmployeePermissionsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}