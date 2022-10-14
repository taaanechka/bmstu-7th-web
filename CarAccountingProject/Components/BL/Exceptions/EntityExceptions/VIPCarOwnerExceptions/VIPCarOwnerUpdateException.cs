using System;

namespace BL
{
    public class VIPCarOwnerUpdateException: Exception
    {
        public VIPCarOwnerUpdateException(): base() {}

        public VIPCarOwnerUpdateException(string? mes): base(mes) {}

        public VIPCarOwnerUpdateException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}