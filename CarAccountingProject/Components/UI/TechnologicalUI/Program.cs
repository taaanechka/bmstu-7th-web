// See https://aka.ms/new-console-template for more information
using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using DB;
using BL;


namespace TechnologicalUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitialMenu();
        }

        static public void InitialMenu()
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\nВыберете действие:\n0. Выйти из приложения\n1. Войти в учетную запись\n");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите логин:");
                        string? login = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");
                        string? password = Console.ReadLine();
                        if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                        {
                            Console.WriteLine("Пустой логин и/или пароль");
                        }
                        else
                        {
                            var authorization = new Authorization();
                            // var authorization = new MySQLAuthorization();
                            authorization.LogIn(login, password);
                        }
                        
                        break;
                    case 0:
                        Console.WriteLine("Выход из приложения выполнен\n");
                        break;
                    default:
                        Console.WriteLine("Введен неверный пункт меню. Повторите попытку\n");
                        break;
                }
            }
        }        
    }
}
