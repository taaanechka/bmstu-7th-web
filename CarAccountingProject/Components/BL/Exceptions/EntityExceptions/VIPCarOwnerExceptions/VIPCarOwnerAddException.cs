using System;

namespace BL
{
    public class VIPCarOwnerAddException: Exception
    {
        public VIPCarOwnerAddException(): base() {}

        public VIPCarOwnerAddException(string? mes): base(mes) {}

        public VIPCarOwnerAddException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}