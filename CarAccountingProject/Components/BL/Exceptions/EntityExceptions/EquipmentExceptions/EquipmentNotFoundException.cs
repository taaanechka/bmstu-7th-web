using System;

namespace BL
{
    public class EquipmentNotFoundException: Exception
    {
        public EquipmentNotFoundException(): base() {}

        public EquipmentNotFoundException(string? mes): base(mes) {}

        public EquipmentNotFoundException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}