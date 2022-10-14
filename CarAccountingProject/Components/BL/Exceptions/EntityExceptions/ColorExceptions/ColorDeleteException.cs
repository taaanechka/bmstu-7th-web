using System;

namespace BL
{
    public class ColorDeleteException: Exception
    {
        public ColorDeleteException(): base() {}

        public ColorDeleteException(string? mes): base(mes) {}

        public ColorDeleteException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}