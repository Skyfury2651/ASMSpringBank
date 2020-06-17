﻿using System;
 using System.Collections;
 using System.Collections.Generic;
 using ConsoleApplication1.Enity;
 using ConsoleApplication1.Helper;
 using ConsoleApplication1.Model;
 using ConsoleApplication3.Resource.View;

 namespace ConsoleApplication2.Controller
{
    public class UserController
    {
        CustomHelper _customHelper = new CustomHelper();
        PasswordHelper _passwordHelper = new PasswordHelper();
        UserModel _userModel = new UserModel();
        public void Deposit(Account account)
        {
            bool false_input = false;
            // Gui tien
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Nhập vào số lượng bạn muốn gửi vào :");
                if (false_input)
                {
                    Console.WriteLine("----- ! Xin hãy nhập vào chữ số ! -----");
                }
                string input = Console.ReadLine();
                bool isDouble = _customHelper.IsNumeric(input);
                if (isDouble)
                {
                    false_input = false;
                    double amount = double.Parse(input);
                    bool status = _userModel.Desposit(account,amount);
                    if (status)
                    {
                        Console.WriteLine("Gửi tiền thành công .");
                        Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Một số lỗi đã xảy ra, vui lòng thử lại sau !");
                        Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                        Console.ReadKey();
                    }
                    break;
                }
                false_input = true;
            }
        }

        public void Withdrawal( Account account)
        {
            bool false_input = false;
            // Gui tien
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Nhập vào số lượng bạn muốn rút :");
                if (false_input)
                {
                    Console.WriteLine("----- ! Xin hãy nhập vào chữ số ! -----");
                }
                string input = Console.ReadLine();
                bool isDouble = _customHelper.IsNumeric(input);
                if (isDouble)
                {
                    false_input = false;
                    double amount = double.Parse(input);
                    bool status = _userModel.Withdrawal(account,amount);
                    if (status)
                    {
                        Console.WriteLine("Rút tiền thành công");
                        Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Một số lỗi đã xảy ra, vui lòng thử lại sau !");
                        Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                        Console.ReadKey();
                    }
                    break;
                }
                false_input = true;
            }
        }

        public void Transfer(Account account)
        {
            bool false_input = false;
            // Gui tien
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Nhập vào AccountNumber của người nhận :");
                var receiverAccountNumber = Console.ReadLine();
                Console.WriteLine("Nhập vào số lượng bạn muốn chuyển khoản : ");
                if (false_input)
                {
                    Console.WriteLine("----- ! Xin hãy nhập vào chữ số ! -----");
                }
                string input = Console.ReadLine();
                bool isDouble = _customHelper.IsNumeric(input);
                if (isDouble)
                {
                    false_input = false;
                    double amount = double.Parse(input);
                    bool status = _userModel.Transfer(account,receiverAccountNumber,amount);
                    if (status)
                    {
                        Console.WriteLine("Chuyển khoản thành công");
                        Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Một số lỗi đã xảy ra, vui lòng thử lại sau !");
                        Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                        Console.ReadKey();
                    }
                    break;
                }
                false_input = true;
            }
        }

        public void BalanceInquiry(Account account)
        {
            double remainBalance = _userModel.BalanceInquiry(account);
            if (remainBalance != null)
            {
                Console.Clear();
                Console.WriteLine("Số tiền còn lại của bạn là : " + remainBalance);
                Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                Console.ReadKey();
            }
        }

        public void ChangePersonalInfo(Account account)
        {
            while (true)
            {
                Console.WriteLine("Nhập vào email của bạn :");
                var email = Console.ReadLine();
                Console.WriteLine("Nhập vào đầy đủ họ và tên :");
                var fullname = Console.ReadLine();
                Console.WriteLine("Nhập vào số điện thoại :");
                var phoneNumber = Console.ReadLine();

                account.Email = email;
                account.Fullname = fullname;
                account.PhoneNumber = phoneNumber;
                bool status = _userModel.ChangePersonalInfo(account);
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

        public bool ChangePassword(Account account)
        {
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
                    account.Salt = _passwordHelper.RandomString(5);
                    account.PasswordHash = _passwordHelper.CreateMD5(newPass+account.Salt);
                    _userModel.ChangePassword(account,newPass);
                    return true;
                }
                Console.WriteLine("Sai mật khẩu");
                Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                Console.ReadKey();
                return false;
        }

        public void transactionHistory(int i,Account account,int currentPage = 1)
        {
           // List<Transaction> list =  _userModel.transactionHisory();
           // List<Transaction> transaction= new UserModel().transactionHisory();
           IList<Transaction> iList= _userModel.transactionHisory(account);
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
           Console.WriteLine("Trang hiện tại : " + currentPage);
           Console.WriteLine("---------");
           // Console.WriteLine("Press '<' to back to previous page");
           Console.WriteLine("Bấm '<' để quay lại trang trước");
           // Console.WriteLine("Press '>' to move to next page");
           Console.WriteLine("Bấm '>' sang trang tiếp theo");
           Console.WriteLine("Bấm 'ESC' để thoát ");
           var keyword = Console.ReadKey(true).Key;
           switch (keyword)
           {
               case ConsoleKey.LeftArrow:
                   i -= pageSize;
                   
                   if (i < 0)
                   {
                       Console.WriteLine("Đây là trang đầu tiên!");
                       Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                       Console.ReadKey();
                   }
                   else
                   {
                       
                       transactionHistory(i,account,currentPage-1);
                   }
                   break;
               case ConsoleKey.RightArrow:
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
                       
                       transactionHistory(i,account,currentPage+1);
                   }
                   
                   break;
               case ConsoleKey.Escape:
                   new Menu().MenuUser(account);
                   break;
               default:
                   Console.WriteLine("Xin hãy chọn đúng !");
                   Console.WriteLine("Nhấn phím bất kỳ để xác nhận");
                   Console.ReadKey();
                   break;
           }
        }
    }
}