using System;

namespace BL
{
    public class BrandsRepositoryNullException: Exception
    {
        public BrandsRepositoryNullException(): base() {}

        public BrandsRepositoryNullException(string? mes): base(mes) {}

        public BrandsRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}