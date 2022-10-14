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
    public class Authorization
    {
        IConfiguration _config;

        public Authorization()
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
                var _context = new DB.ApplicationContext(Connection.GetConnectionString(_config, Connection.DBMS.Postgres, Permissions.UNAUTHORIZED));
                BL.Facade  _facade = new BL.Facade(new RepositoriesFactory(_context));

                BL.User user = _facade.LogIn(login, password);
                OpenConnection(Connection.GetConnectionString(_config, Connection.DBMS.Postgres, user.UserType), _context, user);
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

        public void OpenConnection(string conn, DB.ApplicationContext context, BL.User user)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<StartUp>();
                    services.AddSingleton<Facade>();
                    services.AddSingleton<Presenter>();

                    services.AddDbContext<DB.ApplicationContext>(option => option.UseNpgsql(conn));
                    services.AddSingleton(provider => { return user;});
                } );

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    BL.Facade  _facade = new BL.Facade(new RepositoriesFactory(context));
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