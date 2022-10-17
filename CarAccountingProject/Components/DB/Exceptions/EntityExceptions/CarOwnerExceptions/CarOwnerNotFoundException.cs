using System;

namespace DB
{
    public class CarOwnerNotFoundException: Exception
    {
        public CarOwnerNotFoundException(): base() {}

        public CarOwnerNotFoundException(string? mes): base(mes) {}

        public CarOwnerNotFoundException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}