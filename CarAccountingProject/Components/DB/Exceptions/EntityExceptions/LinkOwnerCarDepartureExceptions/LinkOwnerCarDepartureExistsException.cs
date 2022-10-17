using System;

namespace DB
{
    public class LinkOwnerCarDepartureExistsException: Exception
    {
        public LinkOwnerCarDepartureExistsException(): base() {}

        public LinkOwnerCarDepartureExistsException(string? mes): base(mes) {}

        public LinkOwnerCarDepartureExistsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}