﻿using System;
 using System.Collections;
 using System.Collections.Generic;
 using ConsoleApplication1.Enity;
 using ConsoleApplication1.Helper;
 using ConsoleApplication1.Model;
 using ConsoleApplication3.Resource.View;

 namespace ConsoleApplication2.Controller
{
    public class AdminController
    {
        AdminModel _adminModel = new AdminModel();
        PasswordHelper _passwordHelper = new PasswordHelper();
        

        public void UserList(int i,Account account,int currentPage = 1)
        {
            
            IList<Account> iList= _adminModel.getUserList();
           var pageSize = 3;
           int pageItem = 0;
           if (iList  is IList)
           {
               pageItem = ((IList)iList).Count;
           }
           var Pages = pageItem / pageSize;
           // Console.WriteLine(Pages);    // Số trang
           // Console.WriteLine(pageSize); // Số item trong 1 trang 
           // Console.WriteLine(pageItem); // Tổng số item
           
           Console.WriteLine("-------------------------------------------------");
           Console.WriteLine("ID | AccountNumber | Balance | Username | PasswordHash | Salt | Fullname | Email | PhoneNumber | Role | Status | createAt | updateAt");
           Console.WriteLine("-------------------------------------------------");
           try
           {
               Console.WriteLine(iList[i].ToString());
           }
           catch (Exception e)
           {
           }
           try
           {
               Console.WriteLine(iList[i+1].ToString());
           }
           catch (Exception e)
           {
           }

           try
           {
               Console.WriteLine(iList[i+2].ToString());
           }
           catch (Exception e)
           {
           }
           Console.WriteLine("Tran hiện tại : " + currentPage);
           Console.WriteLine("---------");
           // Console.WriteLine("Press '<' to back to previous page");
           Console.WriteLine("Bấm '<' để quay lại trang trước");
           // Console.WriteLine("Press '>' to move to next page");
           Console.WriteLine("Bấm '>' sang trang tiếp theo");
           Console.WriteLine("Bấm 'ESC' trở về menu");
           var keyword = Console.ReadLine();
           switch (keyword)
           {
               case "<":
                   i -= pageSize;
                   
                   if (i < 0)
                   {
                       Console.WriteLine("Đây là trang đầu tiên!");
                       Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                       Console.ReadKey();
                   }
                   else
                   {
                       
                       UserList(i,account,currentPage-1);
                   }
                   break;
               case ">":
                   i += pageSize;
                   // Console.WriteLine(i);
                   // Console.WriteLine(pageItem);
                   if ((pageItem-i) <= 0)
                   {
                       Console.WriteLine("Đây là trang cuối !");
                       Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                       Console.ReadKey();
                   }
                   else
                   {
                       
                       UserList(i,account,currentPage+1);
                   }
                   
                   break;
               case "ESC":
                   new Menu().MenuAdmin(account);
                   break;
               default:
                   Console.WriteLine("Xin hãy chọn đúng !");
                   Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                   Console.ReadKey();
                   break;
           }
        }

        public void TransactionHistory(int i,Account account,int currentPage = 1)
        {
           // List<Transaction> list =  _userModel.transactionHisory();
           // List<Transaction> transaction= new UserModel().transactionHisory();
           IList<Transaction> iList= _adminModel.TransactionHisory(account);
           var pageSize = 3;
           int pageItem = 0;
           if (iList  is IList)
           {
               pageItem = ((IList)iList).Count;
           }
           var Pages = pageItem / pageSize;
           // Console.WriteLine(Pages);    // Số trang
           // Console.WriteLine(pageSize); // Số item trong 1 trang 
           // Console.WriteLine(pageItem); // Tổng số item
           
           Console.WriteLine("-------------------------------------------------");
           Console.WriteLine("AccountNumber | SenderAccountNumber | ReceiverAccountNumber | Amount | Fee | Message | createAt | updateAt | TransactionStatus");
           Console.WriteLine("-------------------------------------------------");
           try
           {
               Console.WriteLine(iList[i].ToString());
           }
           catch (Exception e)
           {
           }
           try
           {
               Console.WriteLine(iList[i+1].ToString());
           }
           catch (Exception e)
           {
           }

           try
           {
               Console.WriteLine(iList[i+2].ToString());
           }
           catch (Exception e)
           {
           }
           Console.WriteLine("Tran hiện tại : " + currentPage);
           Console.WriteLine("---------");
           // Console.WriteLine("Press '<' to back to previous page");
           Console.WriteLine("Bấm '<' để quay lại trang trước");
           // Console.WriteLine("Press '>' to move to next page");
           Console.WriteLine("Bấm '>' sang trang tiếp theo");
           Console.WriteLine("Bấm 'ESC' trở về menu");
           var keyword = Console.ReadLine();
           switch (keyword)
           {
               case "<":
                   i -= pageSize;
                   
                   if (i < 0)
                   {
                       Console.WriteLine("Đây là trang đầu tiên!");
                       Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                       Console.ReadKey();
                   }
                   else
                   {
                       
                       TransactionHistory(i,account,currentPage-1);
                   }
                   break;
               case ">":
                   i += pageSize;
                   // Console.WriteLine(i);
                   // Console.WriteLine(pageItem);
                   if ((pageItem-i) <= 0)
                   {
                       Console.WriteLine("Đây là trang cuối !");
                       Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                       Console.ReadKey();
                   }
                   else
                   {
                       
                       TransactionHistory(i,account,currentPage+1);
                   }
                   
                   break;
               case "ESC":
                   new Menu().MenuAdmin(account);
                   break;
               default:
                   Console.WriteLine("Xin hãy chọn đúng !");
                   Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                   Console.ReadKey();
                   break;
           }
        }

        public void GetUserByName(Account account)
        {
            Console.WriteLine("Vui lòng nhập vào tên tài khoản bạn muốn tìm kiếm");
            var name = Console.ReadLine();
            List<Account> list = _adminModel.GetUserByName(name);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("ID | AccountNumber | Balance | Username | PasswordHash | Salt | Fullname | Email | PhoneNumber | Role | Status | createAt | updateAt");
            Console.WriteLine("-------------------------------------------------");
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("Bấm bất ký phím nào để tiếp tục");
            Console.ReadKey();
            new Menu().MenuAdmin(account);
        }

        public void GetUserByAccountNumber(Account account)
        {
            Console.WriteLine("Vui lòng nhập vào tên tài khoản bạn muốn tìm kiếm");
            var accountNumber = Console.ReadLine();
            List<Account> list = _adminModel.GetUserByAccountNumber(accountNumber);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("ID | AccountNumber | Balance | Username | PasswordHash | Salt | Fullname | Email | PhoneNumber | Role | Status | createAt | updateAt");
            Console.WriteLine("-------------------------------------------------");
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("Bấm bất ký phím nào để tiếp tục");
            Console.ReadKey();
            new Menu().MenuAdmin(account);
        }

        public void GetUserByPhoneNumber(Account account)
        {
            Console.WriteLine("Vui lòng nhập vào tên tài khoản bạn muốn tìm kiếm");
            var phoneNumber = Console.ReadLine();
            List<Account> list = _adminModel.GetUserByPhoneAccount(phoneNumber);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("ID | AccountNumber | Balance | Username | PasswordHash | Salt | Fullname | Email | PhoneNumber | Role | Status | createAt | updateAt");
            Console.WriteLine("-------------------------------------------------");
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("Bấm bất ký phím nào để tiếp tục");
            Console.ReadKey();
            new Menu().MenuAdmin(account);
        }

        public void AddNewUser(Account account)
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
                
                
                var salt = _passwordHelper.RandomString(3);
                var newaccount = new Account()
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
                _adminModel.Save(newaccount);    
                // Console.WriteLine(account);
            }
            catch (Exception e)
            {
                
            }
        }

        public void LockOpenAccount(Account account)
        {
            try
            {
                Console.WriteLine("Nhập vào account Number của tài khoản bạn muốn thay đổi");
                var AccountNumber = Console.ReadLine();
                Console.WriteLine("Vui lòng chọn hành động !");
                Console.WriteLine("1 - Active | 2 - Deactive | 3 - Banned");
                var choose = Console.ReadLine();
                switch (int.Parse(choose))
                {
                    case 1 :
                        _adminModel.LockOpen(account,AccountNumber,1);
                        break;
                    case 2 :
                        _adminModel.LockOpen(account,AccountNumber,-1);
                        break;
                    case 3 :
                        _adminModel.LockOpen(account,AccountNumber,0);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            new Menu().MenuAdmin(account);
        }

        public void TransactionSearch(int i,Account account,int currentPage = 1)
        {
            Console.WriteLine("Vui lòng nhập mã tài khoản bạn muốn tìm kiếm");
            var AccountNumber = Console.ReadLine();
           // List<Transaction> list =  _userModel.transactionHisory();
           // List<Transaction> transaction= new UserModel().transactionHisory();
           IList<Transaction> iList= _adminModel.transactionHisory(AccountNumber);
           var pageSize = 3;
           int pageItem = 0;
           if (iList  is IList)
           {
               pageItem = ((IList)iList).Count;
           }
           var Pages = pageItem / pageSize;
           // Console.WriteLine(Pages);    // Số trang
           // Console.WriteLine(pageSize); // Số item trong 1 trang 
           // Console.WriteLine(pageItem); // Tổng số item
           
           Console.WriteLine("-------------------------------------------------");
           Console.WriteLine("AccountNumber | SenderAccountNumber | ReceiverAccountNumber | Amount | Fee | Message | createAt | updateAt | TransactionStatus");
           Console.WriteLine("-------------------------------------------------");
           try
           {
               Console.WriteLine(iList[i].ToString());
           }
           catch (Exception e)
           {
           }
           try
           {
               Console.WriteLine(iList[i+1].ToString());
           }
           catch (Exception e)
           {
           }

           try
           {
               Console.WriteLine(iList[i+2].ToString());
           }
           catch (Exception e)
           {
           }
           Console.WriteLine("Tran hiện tại : " + currentPage);
           Console.WriteLine("---------");
           // Console.WriteLine("Press '<' to back to previous page");
           Console.WriteLine("Bấm '<' để quay lại trang trước");
           // Console.WriteLine("Press '>' to move to next page");
           Console.WriteLine("Bấm '>' sang trang tiếp theo");
           var keyword = Console.ReadLine();
           switch (keyword)
           {
               case "<":
                   i -= pageSize;
                   
                   if (i < 0)
                   {
                       Console.WriteLine("Đây là trang đầu tiên!");
                       Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                       Console.ReadKey();
                   }
                   else
                   {
                       
                       TransactionSearch(i,account,currentPage-1);
                   }
                   break;
               case ">":
                   i += pageSize;
                   // Console.WriteLine(i);
                   // Console.WriteLine(pageItem);
                   if ((pageItem-i) <= 0)
                   {
                       Console.WriteLine("Đây là trang cuối !");
                       Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                       Console.ReadKey();
                   }
                   else
                   {
                       
                       TransactionSearch(i,account,currentPage+1);
                   }
                   
                   break;
               case "ESC":
                   new Menu().MenuUser(account);
                   break;
               default:
                   Console.WriteLine("Xin hãy chọn đúng !");
                   Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                   Console.ReadKey();
                   break;
           }
        }

        public void ChangegInform(Account account)
        {
            while (true)
            {
                Account accountNew = new Account();
                Console.WriteLine("Nhập vào mã tài khoản user bạn muốn thay đổi :");
                accountNew.AccountNumber = Console.ReadLine();
                Console.WriteLine("Nhập vào email của bạn :");
                accountNew.Email = Console.ReadLine();
                Console.WriteLine("Nhập vào đầy đủ họ và tên :");
                accountNew.Fullname = Console.ReadLine();
                Console.WriteLine("Nhập vào số điện thoại :");
                accountNew.PhoneNumber = Console.ReadLine();
                
                bool status = _adminModel.ChangePersonalInfo(accountNew);
                if (status)
                {
                    Console.WriteLine("Thay đổi thông tin cá nhân thành công");
                    Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Một số lỗi đã xảy ra, vui lòng thử lại sau !");
                    Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                    Console.ReadKey();
                }
            }
        }

        public void ChangePass(Account account)
        {
            var changeAccount = new Account();
            Console.WriteLine("Nhập vào mã tài khoản bạn muốn thay mật khẩu");
            changeAccount.AccountNumber = Console.ReadLine();
            Console.WriteLine("Nhập vào mật khẩu cũ : ");
            string oldPass = Console.ReadLine();
            bool compare = _passwordHelper.ComparePassword(oldPass,account.Salt,account.PasswordHash);
            if (compare)
            {
                string newPass;
                while (true)
                {
                    Console.WriteLine("Nhập vào mật khẩu mới :");
                    newPass = Console.ReadLine();
                    if (newPass != oldPass)
                    {
                        break;
                    }
                    Console.WriteLine("Xin hãy chọn mật khẩu mới khác mật khẩu cũ");
                }
                changeAccount.Salt = _passwordHelper.RandomString(5);
                changeAccount.PasswordHash = _passwordHelper.CreateMD5(newPass+account.Salt);
                _adminModel.ChangePassword(changeAccount);
            }
            Console.WriteLine("Sai mật khẩu");
            Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
            Console.ReadKey();
        }
    }
}