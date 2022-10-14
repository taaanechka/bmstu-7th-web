using System;

namespace BL
{
    public class EquipmentAddException: Exception
    {
        public EquipmentAddException(): base() {}

        public EquipmentAddException(string? mes): base(mes) {}

        public EquipmentAddException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}