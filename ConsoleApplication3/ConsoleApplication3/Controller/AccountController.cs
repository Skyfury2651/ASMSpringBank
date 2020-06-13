﻿using System;
using ConsoleApplication1.Enity;
using ConsoleApplication1.Helper;
using ConsoleApplication1.Model;

namespace ConsoleApplication2.Controller
{
    public class AccountController
    {
        private AccountModel _accountModel = new AccountModel();
        private PasswordHelper _passwordHelper = new PasswordHelper();
        public Account Login()
        {
            Console.WriteLine("Login....");    
            Console.WriteLine("Please enter your name: ");
            var username = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            var password = Console.ReadLine();
            var account = _accountModel.GetActiveAccountByTheUsername(username);
            if (account != null && _passwordHelper.ComparePassword(password,account.Salt,account.PasswordHash))
            {
                return account;
            }
            return null;
        }
        public bool Register()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Enter Username:");
                var username = Console.ReadLine();
                Console.WriteLine("Enter Your Password :");
                var password = Console.ReadLine();
                Console.WriteLine("Enter your email :");
                var email = Console.ReadLine();
                Console.WriteLine("Enter your fullname :");
                var fullname = Console.ReadLine();
                Console.WriteLine("Enter your phone number :");
                var phoneNumber = Console.ReadLine();
                // Console.WriteLine("Please choose status");
                // var strStatus = Console.ReadLine();
                //
                // var status = int.Parse(strStatus);
                
                var salt = _passwordHelper.RandomString(3);
                var account = new Account()
                    {
                        Username = username,
                        PasswordHash = _passwordHelper.CreateMD5(password + salt),
                        Fullname = fullname,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        Status = Status.ACTIVE,
                        Salt = salt,
                        AccountNumber = _passwordHelper.RandomString(5),
                        Balance = 0,
                        Role = 0
                    };
                _accountModel.Save(account);    
                // Console.WriteLine(account);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}