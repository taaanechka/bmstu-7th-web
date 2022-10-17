using System;

namespace BL
{
    public class ColorNotFoundException: Exception
    {
        public ColorNotFoundException(): base() {}

        public ColorNotFoundException(string? mes): base(mes) {}

        public ColorNotFoundException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}