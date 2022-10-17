using System;

namespace DB
{
    public class ColorExistsException: Exception
    {
        public ColorExistsException(): base() {}

        public ColorExistsException(string? mes): base(mes) {}

        public ColorExistsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}