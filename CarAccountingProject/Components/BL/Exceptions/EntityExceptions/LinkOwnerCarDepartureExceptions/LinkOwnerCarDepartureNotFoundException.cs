using System;

namespace BL
{
    public class LinkOwnerCarDepartureNotFoundException: Exception
    {
        public LinkOwnerCarDepartureNotFoundException(): base() {}

        public LinkOwnerCarDepartureNotFoundException(string? mes): base(mes) {}

        public LinkOwnerCarDepartureNotFoundException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}