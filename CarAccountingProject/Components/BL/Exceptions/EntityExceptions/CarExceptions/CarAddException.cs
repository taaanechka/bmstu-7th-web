using System;

namespace BL
{
    public class CarAddException: Exception
    {
        public CarAddException(): base() {}

        public CarAddException(string? mes): base(mes) {}

        public CarAddException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}