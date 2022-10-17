using System;

namespace BL
{
    public class ModelAddException: Exception
    {
        public ModelAddException(): base() {}

        public ModelAddException(string? mes): base(mes) {}

        public ModelAddException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}