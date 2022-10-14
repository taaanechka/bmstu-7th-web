using System;

namespace BL
{
    public class VIPCarOwnersRepositoryNullException: Exception
    {
        public VIPCarOwnersRepositoryNullException(): base() {}

        public VIPCarOwnersRepositoryNullException(string? mes): base(mes) {}

        public VIPCarOwnersRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}