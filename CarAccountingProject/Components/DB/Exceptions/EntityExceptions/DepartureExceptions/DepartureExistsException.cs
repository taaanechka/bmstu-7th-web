using System;

namespace DB
{
    public class DepartureExistsException: Exception
    {
        public DepartureExistsException(): base() {}

        public DepartureExistsException(string? mes): base(mes) {}

        public DepartureExistsException(string? mes, Exception? innerException) : 
            base(mes, innerException) {}

    }
}