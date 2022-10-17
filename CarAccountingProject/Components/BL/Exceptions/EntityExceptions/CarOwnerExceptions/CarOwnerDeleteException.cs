using System;

namespace BL
{
    public class CarOwnerDeleteException: Exception
    {
        public CarOwnerDeleteException(): base() {}

        public CarOwnerDeleteException(string? mes): base(mes) {}

        public CarOwnerDeleteException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}