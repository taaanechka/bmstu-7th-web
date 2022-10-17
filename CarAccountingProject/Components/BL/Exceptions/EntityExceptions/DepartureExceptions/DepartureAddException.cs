using System;

namespace BL
{
    public class DepartureAddException: Exception
    {
        public DepartureAddException(): base() {}

        public DepartureAddException(string? mes): base(mes) {}

        public DepartureAddException(string? mes, Exception? innerException) : 
            base(mes, innerException) {}

    }
}