using System;

namespace BL
{
    public class EquipmentDeleteException: Exception
    {
        public EquipmentDeleteException(): base() {}

        public EquipmentDeleteException(string? mes): base(mes) {}

        public EquipmentDeleteException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}