using System;

namespace BL
{
    public class ColorsValidatorFailException: Exception
    {
        public ColorsValidatorFailException(): base() {}

        public ColorsValidatorFailException(string? mes): base(mes) {}

        public ColorsValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}