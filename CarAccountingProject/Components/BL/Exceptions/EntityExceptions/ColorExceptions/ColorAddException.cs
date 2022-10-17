using System;

namespace BL
{
    public class ColorAddException: Exception
    {
        public ColorAddException(): base() {}

        public ColorAddException(string? mes): base(mes) {}

        public ColorAddException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}