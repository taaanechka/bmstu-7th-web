using System;

namespace TechnologicalUI.Exceptions
{
    public class AnalystPermissionsException: Exception
    {
        public AnalystPermissionsException(): base() {}

        public AnalystPermissionsException(string? mes): base(mes) {}

        public AnalystPermissionsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}