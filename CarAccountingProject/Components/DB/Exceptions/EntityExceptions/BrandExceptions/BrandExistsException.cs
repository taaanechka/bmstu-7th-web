using System;

namespace DB
{
    public class BrandExistsException: Exception
    {
        public BrandExistsException(): base() {}

        public BrandExistsException(string? mes): base(mes) {}

        public BrandExistsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}