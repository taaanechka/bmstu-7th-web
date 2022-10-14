using System;

namespace BL
{
    public class LinkOwnerCarDepartureAddException: Exception
    {
        public LinkOwnerCarDepartureAddException(): base() {}

        public LinkOwnerCarDepartureAddException(string? mes): base(mes) {}

        public LinkOwnerCarDepartureAddException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}