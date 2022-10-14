using System;

namespace DB
{
    public class CarsValidatorFailException: Exception
    {
        public CarsValidatorFailException(): base() {}

        public CarsValidatorFailException(string? mes): base(mes) {}

        public CarsValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}