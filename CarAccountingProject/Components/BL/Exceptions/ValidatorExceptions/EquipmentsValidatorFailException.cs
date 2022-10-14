using System;

namespace BL
{
    public class EquipmentsValidatorFailException: Exception
    {
        public EquipmentsValidatorFailException(): base() {}

        public EquipmentsValidatorFailException(string? mes): base(mes) {}

        public EquipmentsValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}