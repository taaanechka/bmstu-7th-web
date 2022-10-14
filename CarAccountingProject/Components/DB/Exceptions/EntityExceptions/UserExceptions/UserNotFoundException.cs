using System;

namespace DB
{
    public class UserNotFoundException: Exception
    {
        public UserNotFoundException(): base() {}

        public UserNotFoundException(string? mes): base(mes) {}

        public UserNotFoundException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}