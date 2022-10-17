using System;

namespace BL
{
    public class BrandAddException: Exception
    {
        public BrandAddException(): base() {}

        public BrandAddException(string? mes): base(mes) {}

        public BrandAddException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}