using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using DB;
using BL;

#nullable disable

namespace TechnologicalUI
{
    public class MySQLAuthorization
    {
        IConfiguration _config;

        public MySQLAuthorization()
        {
            string path = Directory.GetCurrentDirectory(); // Components/UI/TechnologicalUI
            _config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(path).ToString()) // Components/UI
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

        public void LogIn(string login, string password)
        {
            try
            {
                var _context = new DB.MySQLApplicationContext(Connection.GetConnectionString(_config, Connection.DBMS.MySQL, Permissions.ADMIN));
                BL.Facade  _facade = new BL.Facade(new MySQLRepositoriesFactory(_context));

                BL.User user = _facade.LogIn(login, password);
                OpenConnection(Connection.GetConnectionString(_config, Connection.DBMS.MySQL, Permissions.ADMIN), _context, user);
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(BL.UserNotFoundException)))
                {
                    Console.WriteLine("Ошибка авторизации. Неверный логин или пароль.");
                }
                else
                {
                    Console.WriteLine("Ошибка авторизации.");
                }
            }
        }

        public void OpenConnection(string conn, DB.MySQLApplicationContext context, BL.User user)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<StartUp>();
                    services.AddSingleton<Facade>();
                    services.AddSingleton<Presenter>();

                    services.AddDbContext<DB.MySQLApplicationContext>(option => option.UseNpgsql(conn));
                    services.AddSingleton(provider => { return user;});
                } );

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    BL.Facade  _facade = new BL.Facade(new MySQLRepositoriesFactory(context));
                    var StartUp = new StartUp(_facade, user);
                    StartUp.Run();
                }
                catch (Exception)
                {
                    Console.WriteLine("Ошибка запуска программы\n");
                }
            }
        }
    }
}