using System;

namespace BL
{
    public class VIPCarOwnersValidatorFailException: Exception
    {
        public VIPCarOwnersValidatorFailException(): base() {}

        public VIPCarOwnersValidatorFailException(string? mes): base(mes) {}

        public VIPCarOwnersValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}