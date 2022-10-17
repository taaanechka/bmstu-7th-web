using System;

namespace DB
{
    public class ModelExistsException: Exception
    {
        public ModelExistsException(): base() {}

        public ModelExistsException(string? mes): base(mes) {}

        public ModelExistsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}