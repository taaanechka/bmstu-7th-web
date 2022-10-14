using System;

namespace BL
{
    public class CarDeleteException: Exception
    {
        public CarDeleteException(): base() {}

        public CarDeleteException(string? mes): base(mes) {}

        public CarDeleteException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}