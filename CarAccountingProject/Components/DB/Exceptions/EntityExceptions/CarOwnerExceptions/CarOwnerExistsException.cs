using System;

namespace DB
{
    public class CarOwnerExistsException: Exception
    {
        public CarOwnerExistsException(): base() {}

        public CarOwnerExistsException(string? mes): base(mes) {}

        public CarOwnerExistsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}