using System;

namespace DB
{
    public class CarOwnersRepositoryNullException: Exception
    {
        public CarOwnersRepositoryNullException(): base() {}

        public CarOwnersRepositoryNullException(string? mes): base(mes) {}

        public CarOwnersRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}