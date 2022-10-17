using System;

namespace DB
{
    public class ComingExistsException: Exception
    {
        public ComingExistsException(): base() {}

        public ComingExistsException(string? mes): base(mes) {}

        public ComingExistsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}