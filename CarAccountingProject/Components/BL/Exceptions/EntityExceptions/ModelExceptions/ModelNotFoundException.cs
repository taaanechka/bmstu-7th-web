using System;

namespace BL
{
    public class ModelNotFoundException: Exception
    {
        public ModelNotFoundException(): base() {}

        public ModelNotFoundException(string? mes): base(mes) {}

        public ModelNotFoundException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}