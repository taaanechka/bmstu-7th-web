using System;

namespace BL
{
    public class EquipmentsRepositoryNullException: Exception
    {
        public EquipmentsRepositoryNullException(): base() {}

        public EquipmentsRepositoryNullException(string? mes): base(mes) {}

        public EquipmentsRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}