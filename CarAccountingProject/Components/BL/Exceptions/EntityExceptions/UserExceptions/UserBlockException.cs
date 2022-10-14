using System;

namespace BL
{
    public class UserBlockException: Exception
    {
        public UserBlockException(): base() {}

        public UserBlockException(string? mes): base(mes) {}

        public UserBlockException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}