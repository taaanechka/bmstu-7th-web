using System;

namespace BL
{
    public class ComingAddException: Exception
    {
        public ComingAddException(): base() {}

        public ComingAddException(string? mes): base(mes) {}

        public ComingAddException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}