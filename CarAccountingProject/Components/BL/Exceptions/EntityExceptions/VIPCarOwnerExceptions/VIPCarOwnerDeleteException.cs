using System;

namespace BL
{
    public class VIPCarOwnerDeleteException: Exception
    {
        public VIPCarOwnerDeleteException(): base() {}

        public VIPCarOwnerDeleteException(string? mes): base(mes) {}

        public VIPCarOwnerDeleteException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}