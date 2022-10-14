using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using BL;

namespace API
{
    public class Program
    {
        // public static string globalConnString = Permissions.UNAUTHORIZED.ToString();

        //  dotnet run --urls http://localhost:5001
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
