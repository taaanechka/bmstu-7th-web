using System;

namespace BL
{
    public class AccessPermissionsException: Exception
    {
        public int? UserId { get; }
        public string? Function { get; }
        public AccessPermissionsException(): base() {}

        public AccessPermissionsException(string? mes): base(mes) {}

        public AccessPermissionsException(string? mes, Exception? innerException): 
            base(mes, innerException) {}

        public AccessPermissionsException(string? mes, int? uId, string? func): base(mes)
        {
            UserId = uId;
            Function = func;
        }
    }
}