using Microsoft.Extensions.Configuration;

using BL;

namespace API
{
    public static class Connection
    {
        public enum DBMS: int
        {
            Postgres = 0,
            MySQL
        }
        
        public static string GetConnectionString(IConfiguration config, DBMS type = DBMS.Postgres, Permissions perm = Permissions.UNAUTHORIZED)
        {
            return config[$"ConnectionStrings{type.ToString()}:{perm.ToString()}"];       
        }
    }
}