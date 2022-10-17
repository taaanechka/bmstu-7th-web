using System;

namespace BL
{
    public class DeparturesRepositoryNullException: Exception
    {
        public DeparturesRepositoryNullException(): base() {}

        public DeparturesRepositoryNullException(string? mes): base(mes) {}

        public DeparturesRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}