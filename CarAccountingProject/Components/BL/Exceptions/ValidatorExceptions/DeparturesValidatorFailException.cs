using System;

namespace BL
{
    public class DeparturesValidatorFailException: Exception
    {
        public DeparturesValidatorFailException(): base() {}

        public DeparturesValidatorFailException(string? mes): base(mes) {}

        public DeparturesValidatorFailException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}