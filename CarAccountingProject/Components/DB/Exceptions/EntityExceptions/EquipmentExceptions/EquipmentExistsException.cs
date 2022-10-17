using System;

namespace DB
{
    public class EquipmentExistsException: Exception
    {
        public EquipmentExistsException(): base() {}

        public EquipmentExistsException(string? mes): base(mes) {}

        public EquipmentExistsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}