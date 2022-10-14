using System;

namespace BL
{
    public class ModelsValidatorFailException: Exception
    {
        public ModelsValidatorFailException(): base() {}

        public ModelsValidatorFailException(string? mes): base(mes) {}

        public ModelsValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}