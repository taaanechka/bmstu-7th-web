using System;

namespace BL
{
    public class ComingDeleteException: Exception
    {
        public ComingDeleteException(): base() {}

        public ComingDeleteException(string? mes): base(mes) {}

        public ComingDeleteException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}