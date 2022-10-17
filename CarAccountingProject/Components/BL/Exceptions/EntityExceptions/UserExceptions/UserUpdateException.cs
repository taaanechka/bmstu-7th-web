using System;

namespace BL
{
    public class UserUpdateException: Exception
    {
        public UserUpdateException(): base() {}

        public UserUpdateException(string? mes): base(mes) {}

        public UserUpdateException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}