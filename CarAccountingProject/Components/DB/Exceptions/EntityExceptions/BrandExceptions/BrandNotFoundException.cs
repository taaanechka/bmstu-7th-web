using System;

namespace DB
{
    public class BrandNotFoundException: Exception
    {
        public BrandNotFoundException(): base() {}

        public BrandNotFoundException(string? mes): base(mes) {}

        public BrandNotFoundException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}