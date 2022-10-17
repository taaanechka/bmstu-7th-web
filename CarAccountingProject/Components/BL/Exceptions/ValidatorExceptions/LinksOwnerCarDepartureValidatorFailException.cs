using System;

namespace BL
{
    public class LinksOwnerCarDepartureValidatorFailException: Exception
    {
        public LinksOwnerCarDepartureValidatorFailException(): base() {}

        public LinksOwnerCarDepartureValidatorFailException(string? mes): base(mes) {}

        public LinksOwnerCarDepartureValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}