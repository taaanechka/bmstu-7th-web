using System;

namespace BL
{
    public class CarsRepositoryNullException: Exception
    {
        public CarsRepositoryNullException(): base() {}

        public CarsRepositoryNullException(string? mes): base(mes) {}

        public CarsRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}