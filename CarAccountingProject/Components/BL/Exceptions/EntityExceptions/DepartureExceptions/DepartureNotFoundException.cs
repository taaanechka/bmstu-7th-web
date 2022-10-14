using System;

namespace BL
{
    public class DepartureNotFoundException: Exception
    {
        public DepartureNotFoundException(): base() {}

        public DepartureNotFoundException(string? mes): base(mes) {}

        public DepartureNotFoundException(string? mes, Exception? innerException) : 
            base(mes, innerException) {}

    }
}