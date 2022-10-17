using System;

namespace BL
{
    public class ModelsRepositoryNullException: Exception
    {
        public ModelsRepositoryNullException(): base() {}

        public ModelsRepositoryNullException(string? mes): base(mes) {}

        public ModelsRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}