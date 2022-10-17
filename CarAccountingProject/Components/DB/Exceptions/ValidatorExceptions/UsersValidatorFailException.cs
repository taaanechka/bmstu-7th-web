using System;

namespace DB
{
    public class UsersValidatorFailException: Exception
    {
        public UsersValidatorFailException(): base() {}

        public UsersValidatorFailException(string? mes): base(mes) {}

        public UsersValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}