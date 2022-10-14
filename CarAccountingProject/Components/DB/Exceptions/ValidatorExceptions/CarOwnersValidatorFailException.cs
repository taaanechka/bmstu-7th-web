using System;

namespace DB
{
    public class CarOwnersValidatorFailException: Exception
    {
        public CarOwnersValidatorFailException(): base() {}

        public CarOwnersValidatorFailException(string? mes): base(mes) {}

        public CarOwnersValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}