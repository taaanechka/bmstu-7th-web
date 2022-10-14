using System;

namespace DB
{
    public class ComingsValidatorFailException: Exception
    {
        public ComingsValidatorFailException(): base() {}

        public ComingsValidatorFailException(string? mes): base(mes) {}

        public ComingsValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}