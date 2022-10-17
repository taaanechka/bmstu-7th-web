using System;

namespace BL
{
    public class ModelDeleteException: Exception
    {
        public ModelDeleteException(): base() {}

        public ModelDeleteException(string? mes): base(mes) {}

        public ModelDeleteException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}