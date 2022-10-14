using System;

namespace DB
{
    public class ModelsRepositoryNullException: Exception
    {
        public ModelsRepositoryNullException(): base() {}

        public ModelsRepositoryNullException(string? mes): base(mes) {}

        public ModelsRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}