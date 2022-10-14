using System;

namespace DB
{
    public class ComingsRepositoryNullException: Exception
    {
        public ComingsRepositoryNullException(): base() {}

        public ComingsRepositoryNullException(string? mes): base(mes) {}

        public ComingsRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}