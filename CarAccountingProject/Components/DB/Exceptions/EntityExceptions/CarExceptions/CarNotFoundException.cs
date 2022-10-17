using System;

namespace DB
{
    public class CarNotFoundException: Exception
    {
        public CarNotFoundException(): base() {}

        public CarNotFoundException(string? mes): base(mes) {}

        public CarNotFoundException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}