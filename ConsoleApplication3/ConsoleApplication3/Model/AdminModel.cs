using System;
using System.Collections.Generic;
using ConsoleApplication1.Enity;
using ConsoleApplication1.Helper;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1.Model
{
    public class AdminModel
    {
        public List<Account> getUserList()
        {
            Account account = null;
            var cnn = ConnectionHelper.getConnection();
            List<Account> accountList = new List<Account>();
            cnn.Open();
            var cmd = new MySqlCommand($"select * from `useraccount` where Role = 0", cnn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                account = new Account()
                {
                    ID = reader.GetString("ID"),
                    AccountNumber = reader.GetString("AccountNumber"),
                    Balance = reader.GetDouble("Balance"),
                    Username = reader.GetString("Username"),
                    PasswordHash = reader.GetString("PasswordHash"),
                    Salt = reader.GetString("Salt"),
                    Fullname = reader.GetString("Fullname"),
                    Email = reader.GetString("Email"),
                    PhoneNumber = reader.GetString("PhoneNumber"),
                    Status = (Status) reader.GetInt32("Status"),
                    Role = (Role) reader.GetUInt32("Role"),
                    CreatedAt = reader.GetMySqlDateTime("CreatedAt").ToString(),
                    UpdatedAt = reader.GetMySqlDateTime("UpdatedAt").ToString()
                };
                accountList.Add(account);
            }

            reader.Close();
            cnn.Close();
            return accountList;
        }

        public IList<Transaction> TransactionHisory(Account account)
        {
            try
            {
                // 1. Kết nối database
                var cnn = ConnectionHelper.getConnection();
                cnn.Open();
                // 2. Tạo transaction
                // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
                var stringCmdGetAccount = $"Select * from `transaction_history`";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var reader = cmdGetAccount.ExecuteReader();
                List<Transaction> list = new List<Transaction>();
                while (reader.Read())
                {
                    try
                    {
                        list.Add(new Transaction()
                        {
                            TransactionCode = reader.GetString("TransactionCode"),
                            SenderAccountNumber = reader.GetString("SenderAccountNumber"),
                            ReceiverAccountNumber = reader.GetString("ReceiverAccountNumber"),
                            Fee = reader.GetDouble("Fee"),
                            Message = reader.GetString("Message"),
                            Amount = reader.GetDouble("Amount"),
                            CreatedAt = (DateTime) reader.GetMySqlDateTime("CreatedAt"),
                            UpdatedAt = (DateTime) reader.GetMySqlDateTime("UpdatedAt"),
                            TransactionStatus = (TransactionStatus) reader.GetInt32("TransactionStatus"),
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Account> GetUserByName(string name)
        {
            try
            {
                // 1. Kết nối database
                var cnn = ConnectionHelper.getConnection();
                cnn.Open();
                // 2. Tạo transaction
                // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
                var stringCmdGetAccount = $"Select * from `useraccount` where Fullname = '{name}'";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var reader = cmdGetAccount.ExecuteReader();
                List<Account> list = new List<Account>();
                while (reader.Read())
                {
                    try
                    {
                        list.Add(new Account()
                        {
                            ID = reader.GetString("ID"),
                            AccountNumber = reader.GetString("AccountNumber"),
                            Balance = reader.GetDouble("Balance"),
                            Username = reader.GetString("Username"),
                            PasswordHash = reader.GetString("PasswordHash"),
                            Salt = reader.GetString("Salt"),
                            Fullname = reader.GetString("Fullname"),
                            Email = reader.GetString("Email"),
                            PhoneNumber = reader.GetString("PhoneNumber"),
                            Status = (Status) reader.GetInt32("Status"),
                            Role = (Role) reader.GetUInt32("Role"),
                            CreatedAt = reader.GetMySqlDateTime("CreatedAt").ToString(),
                            UpdatedAt = reader.GetMySqlDateTime("UpdatedAt").ToString()
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Account> GetUserByAccountNumber(string accountNumber)
        {
            try
            {
                // 1. Kết nối database
                var cnn = ConnectionHelper.getConnection();
                cnn.Open();
                // 2. Tạo transaction
                // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
                var stringCmdGetAccount = $"Select * from `useraccount` where `AccountNumber` = '{accountNumber}'";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var reader = cmdGetAccount.ExecuteReader();
                List<Account> list = new List<Account>();
                while (reader.Read())
                {
                    try
                    {
                        list.Add(new Account()
                        {
                            ID = reader.GetString("ID"),
                            AccountNumber = reader.GetString("AccountNumber"),
                            Balance = reader.GetDouble("Balance"),
                            Username = reader.GetString("Username"),
                            PasswordHash = reader.GetString("PasswordHash"),
                            Salt = reader.GetString("Salt"),
                            Fullname = reader.GetString("Fullname"),
                            Email = reader.GetString("Email"),
                            PhoneNumber = reader.GetString("PhoneNumber"),
                            Status = (Status) reader.GetInt32("Status"),
                            Role = (Role) reader.GetUInt32("Role"),
                            CreatedAt = reader.GetMySqlDateTime("CreatedAt").ToString(),
                            UpdatedAt = reader.GetMySqlDateTime("UpdatedAt").ToString()
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Account> GetUserByPhoneAccount(string phoneNumber)
        {
            try
            {
                // 1. Kết nối database
                var cnn = ConnectionHelper.getConnection();
                cnn.Open();
                // 2. Tạo transaction
                // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
                var stringCmdGetAccount = $"Select * from `useraccount` where `PhoneNumber` = '{phoneNumber}'";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var reader = cmdGetAccount.ExecuteReader();
                List<Account> list = new List<Account>();
                while (reader.Read())
                {
                    try
                    {
                        list.Add(new Account()
                        {
                            ID = reader.GetString("ID"),
                            AccountNumber = reader.GetString("AccountNumber"),
                            Balance = reader.GetDouble("Balance"),
                            Username = reader.GetString("Username"),
                            PasswordHash = reader.GetString("PasswordHash"),
                            Salt = reader.GetString("Salt"),
                            Fullname = reader.GetString("Fullname"),
                            Email = reader.GetString("Email"),
                            PhoneNumber = reader.GetString("PhoneNumber"),
                            Status = (Status) reader.GetInt32("Status"),
                            Role = (Role) reader.GetUInt32("Role"),
                            CreatedAt = reader.GetMySqlDateTime("CreatedAt").ToString(),
                            UpdatedAt = reader.GetMySqlDateTime("UpdatedAt").ToString()
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void Save(Account account)
        {
            var cnn = ConnectionHelper.getConnection();
            DateTime dateTime = DateTime.Now;
            String currentDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            cnn.Open();
            var stringCommand =
                $"INSERT INTO `useraccount`(`ID`, `AccountNumber`, `Balance`, `Username`, `PasswordHash`, `Salt`, `Fullname`, `Email`, `PhoneNumber`, `Status`, `CreatedAt`, `UpdatedAt`) VALUES (NULL, '{account.AccountNumber}','{account.Balance}','{account.Username}','{account.PasswordHash}','{account.Salt}','{account.Fullname}','{account.Email}','{account.PhoneNumber}', '{(int) account.Status}', '{currentDate}', '{currentDate}')";
            MySqlCommand cmd =
                new MySqlCommand(stringCommand, cnn);
            // Console.WriteLine(stringCommand);
            cmd.ExecuteNonQuery();
            cnn.Close();
            Console.WriteLine("\nCreated Account Success");
        }

        public void LockOpen(Account account, string accountNumber, int status)
        {
            try
            {
                var cnn = ConnectionHelper.getConnection();
                DateTime dateTime = DateTime.Now;
                String currentDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                cnn.Open();
                // Console.WriteLine(status);
                var stringCommand =
                    $"UPDATE `useraccount` SET `Status` = '{status}' WHERE `useraccount`.`AccountNumber` = '{accountNumber}';";
                MySqlCommand cmd =
                    new MySqlCommand(stringCommand, cnn);
                cmd.ExecuteNonQuery();
                // Console.WriteLine(stringCommand);
                cnn.Close();
                Console.WriteLine("Thay đổi thành công");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IList<Transaction> transactionHisory(string accountNumber)
        {
            try
            {
                // 1. Kết nối database
                var cnn = ConnectionHelper.getConnection();
                cnn.Open();
                // 2. Tạo transaction
                // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
                var stringCmdGetAccount =
                    $"Select * from `transaction_history` WHERE `SenderAccountNumber` = '{accountNumber}' OR `ReceiverAccountNumber` = '{accountNumber}'";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var reader = cmdGetAccount.ExecuteReader();
                List<Transaction> list = new List<Transaction>();
                while (reader.Read())
                {
                    try
                    {
                        list.Add(new Transaction()
                        {
                            TransactionCode = reader.GetString("TransactionCode"),
                            SenderAccountNumber = reader.GetString("SenderAccountNumber"),
                            ReceiverAccountNumber = reader.GetString("ReceiverAccountNumber"),
                            Fee = reader.GetDouble("Fee"),
                            Message = reader.GetString("Message"),
                            Amount = reader.GetDouble("Amount"),
                            CreatedAt = (DateTime) reader.GetMySqlDateTime("CreatedAt"),
                            UpdatedAt = (DateTime) reader.GetMySqlDateTime("UpdatedAt"),
                            TransactionStatus = (TransactionStatus) reader.GetInt32("TransactionStatus"),
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool ChangePersonalInfo(Account account)
        {
            try
            {
                // 1. Kết nối database
                var cnn = ConnectionHelper.getConnection();
                cnn.Open();
                // 2. Tạo transaction
                // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
                var stringCmdGetAccount =
                    $"UPDATE `useraccount` SET `Fullname` = '{account.Fullname}', `Email` = '{account.Email}', `PhoneNumber` = '{account.PhoneNumber}' WHERE `AccountNumber` = '{account.AccountNumber}';";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                cmdGetAccount.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void ChangePassword(Account account)
        {
            try
            {
                // 1. Kết nối database
                var cnn = ConnectionHelper.getConnection();
                cnn.Open();
                // 2. Tạo transaction
                var stringCmdGetAccount =
                    $"UPDATE `useraccount` SET `PasswordHash` = '{account.PasswordHash}', `Salt` = '{account.Salt}' WHERE `AccountNumber` = '{account.AccountNumber}';";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                cmdGetAccount.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception e)
            {
            }
        }
    }
}