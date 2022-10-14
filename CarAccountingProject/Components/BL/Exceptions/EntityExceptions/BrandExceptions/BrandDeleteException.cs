using System;

namespace BL
{
    public class BrandDeleteException: Exception
    {
        public BrandDeleteException(): base() {}

        public BrandDeleteException(string? mes): base(mes) {}

        public BrandDeleteException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}