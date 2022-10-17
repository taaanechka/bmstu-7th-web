using System;

namespace BL
{
    public class EquipmentUpdateException: Exception
    {
        public EquipmentUpdateException(): base() {}

        public EquipmentUpdateException(string? mes): base(mes) {}

        public EquipmentUpdateException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}