using System;

namespace BL
{
    public class BrandUpdateException: Exception
    {
        public BrandUpdateException(): base() {}

        public BrandUpdateException(string? mes): base(mes) {}

        public BrandUpdateException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}