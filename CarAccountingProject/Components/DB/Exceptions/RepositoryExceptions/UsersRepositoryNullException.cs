using System;

namespace DB
{
    public class UsersRepositoryNullException: Exception
    {
        public UsersRepositoryNullException(): base() {}

        public UsersRepositoryNullException(string? mes): base(mes) {}

        public UsersRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}