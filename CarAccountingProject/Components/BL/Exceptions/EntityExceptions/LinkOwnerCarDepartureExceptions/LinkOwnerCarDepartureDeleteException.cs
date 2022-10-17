using System;

namespace BL
{
    public class LinkOwnerCarDepartureDeleteException: Exception
    {
        public LinkOwnerCarDepartureDeleteException(): base() {}

        public LinkOwnerCarDepartureDeleteException(string? mes): base(mes) {}

        public LinkOwnerCarDepartureDeleteException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}