using System;

namespace DB
{
    public class ComingNotFoundException: Exception
    {
        public ComingNotFoundException(): base() {}

        public ComingNotFoundException(string? mes): base(mes) {}

        public ComingNotFoundException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}