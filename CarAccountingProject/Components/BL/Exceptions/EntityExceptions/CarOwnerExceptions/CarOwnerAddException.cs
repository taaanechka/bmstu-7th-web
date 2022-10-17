using System;

namespace BL
{
    public class CarOwnerAddException: Exception
    {
        public CarOwnerAddException(): base() {}

        public CarOwnerAddException(string? mes): base(mes) {}

        public CarOwnerAddException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}