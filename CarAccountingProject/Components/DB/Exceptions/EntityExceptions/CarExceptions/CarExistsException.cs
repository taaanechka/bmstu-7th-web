using System;

namespace DB
{
    public class CarExistsException: Exception
    {
        public CarExistsException(): base() {}

        public CarExistsException(string? mes): base(mes) {}

        public CarExistsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}