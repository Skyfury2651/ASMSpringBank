using System;
using ConsoleApplication1.Enity;
using ConsoleApplication2.Controller;

namespace ConsoleApplication3.Resource.View
{
    public class Menu
    {
        // [DllImport("kernel32.dll")]
        // static extern bool SetConsoleOutputCP(uint wCodePageID);

        public void BeginMenu()
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
                var choose = int.Parse(Console.ReadLine());
                switch (choose)
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
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
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

        public void MenuAdmin(Account account)
        {
            AdminController _adminController = new AdminController();
            Console.WriteLine("\n");
            Console.WriteLine("_________________________ Spring Hero Bank _________________________");
            Console.WriteLine("Chào mừng Admin " + account.Fullname + " quay trở lại. Vui lòng chọn thao tác.");
            Console.WriteLine("1. Danh sách người dùng.");
            Console.WriteLine("2. Danh sách lịch sử giao dịch.");
            Console.WriteLine("3. Tìm kiếm người dùng theo tên.");
            Console.WriteLine("4. Tìm kiếm người dùng theo số tài khoản.");
            Console.WriteLine("5. Tìm kiếm người dùng theo số điện thoại.");
            Console.WriteLine("6. Thêm người dùng mới.");
            Console.WriteLine("7. Khoá và mở tài khoản người dùng.");
            Console.WriteLine("8. Tìm kiếm lịch sử giao dịch theo số tài khoản.");
            Console.WriteLine("9. Thay đổi thông tin tài khoản.");
            Console.WriteLine("10. Thay đổi thông tin mật khẩu.");
            Console.WriteLine("11. Thoát.");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 11):");
            var choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Danh sách người dùng.");
                    _adminController.UserList(0, account);
                    break;
                case 2:
                    Console.WriteLine("Danh sách lịch sử giao dịch");
                    _adminController.TransactionHistory(0, account);
                    break;
                case 3:
                    Console.WriteLine("Tìm kiếm tài khoản theo tên");
                    _adminController.GetUserByName(account);
                    break;
                case 4:
                    Console.WriteLine("Tìm kiếm tài khoản theo AccountNumber");
                    _adminController.GetUserByAccountNumber(account);
                    break;
                case 5:
                    Console.WriteLine("Tìm kiếm tài khoản theo số điện thoại");
                    _adminController.GetUserByPhoneNumber(account);
                    break;
                case 6:
                    Console.WriteLine("Thêm tài khoản mơi");
                    _adminController.AddNewUser(account);
                    break;
                case 7:
                    Console.WriteLine("Khóa , Mở tài khoản");
                    _adminController.LockOpenAccount(account);
                    break;
                case 8:
                    Console.WriteLine("Search transaction history by account number");
                    _adminController.TransactionSearch(0, account);
                    break;
                case 9:
                    Console.WriteLine("Change the account information");
                    _adminController.ChangeInfo(account);
                    break;
                case 10:
                    Console.WriteLine("Change password information");
                    _adminController.ChangePass(account);
                    break;
                case 11:
                    break;
            }
        }

        public void MenuUser(Account account)
        {
            UserController userController = new UserController();
            Console.WriteLine("\n");
            Console.WriteLine("_________________________ Spring Hero Bank _________________________");
            Console.WriteLine("Chào mừng " + account.Fullname + " quay trở lại. Vui lòng chọn thao tác.");
            Console.WriteLine("1. Gửi tiền.");
            Console.WriteLine("2. Rút tiền.");
            Console.WriteLine("3. Chuyển khoản.");
            Console.WriteLine("4. Truy vấn số dư.");
            Console.WriteLine("5. Thay đổi thông tin cá nhân.");
            Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
            Console.WriteLine("7. Truy vấn lịch sử giao dịch.");
            Console.WriteLine("8. Thoát.");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 8):");
            var choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Gửi tiền");
                    userController.Deposit(account);
                    break;
                case 2:
                    Console.WriteLine("Rút tiền");
                    userController.Withdrawal(account);
                    break;
                case 3:
                    Console.WriteLine("Chuyển khoản");
                    userController.Transfer(account);
                    break;
                case 4:
                    Console.WriteLine("Truy vấn số dư.");
                    userController.BalanceInquiry(account);
                    break;
                case 5:
                    Console.WriteLine("Thay đổi thông tin cá nhân.");
                    userController.ChangePersonalInfo(account);
                    break;
                case 6:
                    Console.WriteLine("Thay đổi thông tin mật khẩu.");
                    userController.ChangePassword(account);
                    break;
                case 7:
                    Console.WriteLine("Truy vấn lịch sử giao dịch.");
                    userController.transactionHistory(0, account);
                    break;
                case 8:
                    break;
            }
        }
    }
}