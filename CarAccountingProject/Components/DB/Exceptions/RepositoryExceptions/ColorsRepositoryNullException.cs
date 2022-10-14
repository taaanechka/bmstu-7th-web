using System;

namespace DB
{
    public class ColorsRepositoryNullException: Exception
    {
        public ColorsRepositoryNullException(): base() {}

        public ColorsRepositoryNullException(string? mes): base(mes) {}

        public ColorsRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}