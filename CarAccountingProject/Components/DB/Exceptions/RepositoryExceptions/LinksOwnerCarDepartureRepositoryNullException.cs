using System;

namespace DB
{
    public class LinksOwnerCarDepartureRepositoryNullException: Exception
    {
        public LinksOwnerCarDepartureRepositoryNullException(): base() {}

        public LinksOwnerCarDepartureRepositoryNullException(string? mes): base(mes) {}

        public LinksOwnerCarDepartureRepositoryNullException(string? mes, Exception? innerException): 
            base(mes, innerException) {}
    }
}