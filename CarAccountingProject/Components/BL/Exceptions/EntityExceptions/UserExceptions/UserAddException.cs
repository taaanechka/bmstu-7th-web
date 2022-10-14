using System;

namespace BL
{
    public class UserAddException: Exception
    {
        public UserAddException(): base() {}

        public UserAddException(string? mes): base(mes) {}

        public UserAddException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}