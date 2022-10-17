using System;

namespace BL
{
    public class ColorUpdateException: Exception
    {
        public ColorUpdateException(): base() {}

        public ColorUpdateException(string? mes): base(mes) {}

        public ColorUpdateException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}