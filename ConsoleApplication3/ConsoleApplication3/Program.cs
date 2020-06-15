using System;
using ConsoleApplication1.Enity;
using ConsoleApplication2.Controller;
using ConsoleApplication3.Resource.View;

namespace ConsoleApplication3
{
    internal class Program
    {
        // [DllImport("kernel32.dll")]
        // static extern bool SetConsoleOutputCP(uint wCodePageID);

        public static void Main(string[] args)
        {
            // SetConsoleOutputCP(65001);
            Account currentAccount = null;
            while (currentAccount == null)
            {
                Console.WriteLine("\n");
                Console.WriteLine(DateTime.Now);
                Console.WriteLine("—— Spring Hero Bank ——");
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Register.");
                Console.WriteLine("2. Login.");
                Console.WriteLine("3. Exit.");
                Console.WriteLine("----------------------");
                var choose = Console.ReadLine();
                switch ((int.Parse(choose)))
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Register Form");
                        new AccountController().Register();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Login Form");
                        Account loginAccount = new AccountController().Login();
                        currentAccount = loginAccount;
                        // Console.WriteLine(loginAccount);
                        // Console.WriteLine(currentAccount);
                        break;
                    default:
                        // Console.WriteLine("Error");
                        // Console.WriteLine("choose");
                        break;
                }
            }

            // Console.WriteLine(currentAccount.Role);
            // Console.WriteLine((int)currentAccount.Role);
            if ((int) currentAccount.Role == 1)
            {
                Console.Clear();
                new Menu().MenuAdmin(currentAccount);
            }
            else
            {
                while (true)
                {
                    new Menu().MenuUser(currentAccount);
                }
            }
        }
    }
}