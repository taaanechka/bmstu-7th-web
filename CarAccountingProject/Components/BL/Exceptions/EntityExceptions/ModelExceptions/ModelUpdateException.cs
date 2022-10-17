using System;

namespace BL
{
    public class ModelUpdateException: Exception
    {
        public ModelUpdateException(): base() {}

        public ModelUpdateException(string? mes): base(mes) {}

        public ModelUpdateException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}