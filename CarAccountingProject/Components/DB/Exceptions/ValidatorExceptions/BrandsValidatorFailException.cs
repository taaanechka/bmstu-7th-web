using System;

namespace DB
{
    public class BrandsValidatorFailException: Exception
    {
        public BrandsValidatorFailException(): base() {}

        public BrandsValidatorFailException(string? mes): base(mes) {}

        public BrandsValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}