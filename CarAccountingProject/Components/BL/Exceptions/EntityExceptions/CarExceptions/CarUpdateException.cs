using System;

namespace BL
{
    public class CarUpdateException: Exception
    {
        public CarUpdateException(): base() {}

        public CarUpdateException(string? mes): base(mes) {}

        public CarUpdateException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}