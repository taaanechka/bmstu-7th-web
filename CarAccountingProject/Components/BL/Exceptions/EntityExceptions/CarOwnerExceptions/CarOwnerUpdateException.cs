using System;

namespace BL
{
    public class CarOwnerUpdateException: Exception
    {
        public CarOwnerUpdateException(): base() {}

        public CarOwnerUpdateException(string? mes): base(mes) {}

        public CarOwnerUpdateException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}