using System;

namespace BL
{
    public class DepartureDeleteException: Exception
    {
        public DepartureDeleteException(): base() {}

        public DepartureDeleteException(string? mes): base(mes) {}

        public DepartureDeleteException(string? mes, Exception? innerException) : 
            base(mes, innerException) {}

    }
}