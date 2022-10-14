using System;

namespace DB
{
    public class UserExistsException: Exception
    {
        public UserExistsException(): base() {}

        public UserExistsException(string? mes): base(mes) {}

        public UserExistsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}