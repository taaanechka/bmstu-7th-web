using System;

namespace BL
{
    public class VIPCarOwnerNotFoundException: Exception
    {
        public VIPCarOwnerNotFoundException(): base() {}

        public VIPCarOwnerNotFoundException(string? mes): base(mes) {}

        public VIPCarOwnerNotFoundException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}