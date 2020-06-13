using System;
using System.Collections.Generic;
using ConsoleApplication1.Enity;
using ConsoleApplication1.Helper;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1.Model
{
    public class UserModel
    {
        public bool Desposit(Account account, double amount)
        {
            // 1. Kết nối database
            var cnn = ConnectionHelper.getConnection();
            cnn.Open();
            // 2. Tạo transaction
            var transaction = cnn.BeginTransaction();
            // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
            try
            {
                if (amount <= 0)
                {
                    Console.WriteLine("Invalid amount !");
                    return false;
                }
                var stringCmdGetAccount = $"SELECT balance FROM `useraccount` WHERE AccountNumber = '{account.AccountNumber}' AND Status = {(int) Status.ACTIVE}";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount,cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    accountReader.Close(); // nó ko đóng ở đây. trong trường hợp lỗi. thử account khác đi. va
                    //rollback
                    throw new Exception("Account is not found or has been deleted!");
                }

                // Console.WriteLine("Okie account");
                var currentBalance = accountReader.GetDouble("balance");
                // Console.WriteLine(currentBalance);
                accountReader.Close();

                currentBalance += amount;
                var stringCmdUpdateAccount = $"update `useraccount` set balance = {currentBalance} where AccountNumber = '{account.AccountNumber}'";
                var cmdUpdateAccount = new MySqlCommand(stringCmdUpdateAccount,cnn);
                cmdUpdateAccount.ExecuteNonQuery();
                
                // 2.3 Lưu transaction history
                var trasTransaction = new Transaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = account.AccountNumber,
                    ReceiverAccountNumber = account.AccountNumber,
                    Type = TransactionType.DEDPOSIT,
                    Amount = amount,
                    Fee = 0,
                    Message = "Deposited : " + amount,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    TransactionStatus = TransactionStatus.DONE
                };
                
                var stringCmdInsertTransaction = "INSERT INTO `transaction_history`(`ID`,`TransactionCode`, `SenderAccountNumber`, `ReceiverAccountNumber`, `Type`, `Amount`, `Fee`, `Message`, `CreateAt`, `UpdateAt`, `TransactionStatus`) " +
                                                 $"VALUES ('NULL','{trasTransaction.TransactionCode}','{trasTransaction.SenderAccountNumber}','{trasTransaction.ReceiverAccountNumber}','{trasTransaction.Type}','{trasTransaction.Amount}','{trasTransaction.Fee}','{trasTransaction.Message}','{trasTransaction.CreateAt.ToString("yyyy-MM-dd HH:mm:ss")}','{trasTransaction.UpdateAt.ToString("yyyy-MM-dd HH:mm:ss")}','{(int) trasTransaction.TransactionStatus}')";
                // 3.1 Commit nếu tất cả đều thành công
                var cmdInsertTransaction = new MySqlCommand(stringCmdInsertTransaction,cnn);
                cmdInsertTransaction.ExecuteNonQuery();
                
                transaction.Commit();
                cnn.Close();
                return true;
            }
            // 3.2 Rollback nếu có sự cố
            catch (Exception e)
            {
                transaction.Rollback();
                throw;
            }
            // 2.2 Update số dư tài khoản
            return false;
        }
        public bool Withdrawal(Account account, double amount)
        {
            // 1. Kết nối database
            var cnn = ConnectionHelper.getConnection();
            cnn.Open();
            // 2. Tạo transaction
            var transaction = cnn.BeginTransaction();
            // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
            try
            {
                if (amount <= 0)
                {
                    Console.WriteLine("Invalid amount !");
                    return false;
                }
                var stringCmdGetAccount = $"SELECT balance FROM `useraccount` WHERE AccountNumber = '{account.AccountNumber}' AND Status = {(int) Status.ACTIVE}";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount,cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    accountReader.Close(); // nó ko đóng ở đây. trong trường hợp lỗi. thử account khác đi. va
                    //rollback
                    throw new Exception("Account is not found or has been deleted!");
                }

                // Console.WriteLine("Okie account");
                var currentBalance = accountReader.GetDouble("balance");
                // Console.WriteLine(currentBalance);
                accountReader.Close();

                currentBalance -= amount;
                if (currentBalance < 0)
                {
                    Console.WriteLine("The remaining amount is not enough to take withdrawal !");
                    return false;
                }
                var stringCmdUpdateAccount = $"update `useraccount` set balance = {currentBalance} where AccountNumber = '{account.AccountNumber}'";
                var cmdUpdateAccount = new MySqlCommand(stringCmdUpdateAccount,cnn);
                cmdUpdateAccount.ExecuteNonQuery();
                
                // 2.3 Lưu transaction history
                var trasTransaction = new Transaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = account.AccountNumber,
                    ReceiverAccountNumber = account.AccountNumber,
                    Type = TransactionType.DEDPOSIT,
                    Amount = amount,
                    Fee = 0,
                    Message = "Withdrawal : " + amount,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    TransactionStatus = TransactionStatus.DONE
                };
                
                var stringCmdInsertTransaction = "INSERT INTO `transaction_history`(`ID`,`TransactionCode`, `SenderAccountNumber`, `ReceiverAccountNumber`, `Type`, `Amount`, `Fee`, `Message`, `CreateAt`, `UpdateAt`, `TransactionStatus`) " +
                                                 $"VALUES ('NULL','{trasTransaction.TransactionCode}','{trasTransaction.SenderAccountNumber}','{trasTransaction.ReceiverAccountNumber}','{trasTransaction.Type}','{trasTransaction.Amount}','{trasTransaction.Fee}','{trasTransaction.Message}','{trasTransaction.CreateAt.ToString("yyyy-MM-dd HH:mm:ss")}','{trasTransaction.UpdateAt.ToString("yyyy-MM-dd HH:mm:ss")}','{(int) trasTransaction.TransactionStatus}')";
                // 3.1 Commit nếu tất cả đều thành công
                var cmdInsertTransaction = new MySqlCommand(stringCmdInsertTransaction,cnn);
                cmdInsertTransaction.ExecuteNonQuery();
                
                transaction.Commit();
                cnn.Close();
                return true;
            }
            // 3.2 Rollback nếu có sự cố
            catch (Exception e)
            {
                transaction.Rollback();
                throw;
            }
            // 2.2 Update số dư tài khoản
            return false;
        }
        public bool Transfer(Account account,string receiverAccountNumber, double amount)
        {
            // 1. Kết nối database
            var cnn = ConnectionHelper.getConnection();
            cnn.Open();
            // 2. Tạo transaction
            var transaction = cnn.BeginTransaction();
            // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
            try
            {
                if (amount <= 0)
                {
                    Console.WriteLine("Invalid amount !");
                    return false;
                }

                try
                {
                    var stringCmdGetAccount = $"SELECT balance FROM `useraccount` WHERE AccountNumber = '{account.AccountNumber}' AND Status = {(int) Status.ACTIVE}";
                    var cmdGetAccount = new MySqlCommand(stringCmdGetAccount,cnn);
                    var accountReader = cmdGetAccount.ExecuteReader();
                    if (!accountReader.Read())
                    {
                        accountReader.Close(); // nó ko đóng ở đây. trong trường hợp lỗi. thử account khác đi. va
                        //rollback
                        throw new Exception("Account is not found or has been deleted!");
                    }

                    // Console.WriteLine("Okie account");
                    var currentBalance = accountReader.GetDouble("balance");
                    // Console.WriteLine(currentBalance);
                    accountReader.Close();

                    currentBalance -= amount;
                    if (currentBalance < 0)
                    {
                        Console.WriteLine("The remaining amount is not enough to take withdrawal !");
                        return false;
                    }
                    var stringCmdUpdateAccount = $"update `useraccount` set balance = {currentBalance} where AccountNumber = '{account.AccountNumber}'";
                    var cmdUpdateAccount = new MySqlCommand(stringCmdUpdateAccount,cnn);
                    cmdUpdateAccount.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                try
                {
                    var stringCmdGetAccount = $"SELECT balance FROM `useraccount` WHERE AccountNumber = '{receiverAccountNumber}' AND Status = {(int) Status.ACTIVE}";
                    var cmdGetAccount = new MySqlCommand(stringCmdGetAccount,cnn);
                    var accountReader = cmdGetAccount.ExecuteReader();
                    if (!accountReader.Read())
                    {
                        accountReader.Close(); // nó ko đóng ở đây. trong trường hợp lỗi. thử account khác đi. va
                        //rollback
                        throw new Exception("Account is not found or has been deleted!");
                    }

                    // Console.WriteLine("Okie account");
                    var currentBalance = accountReader.GetDouble("balance");
                    // Console.WriteLine(currentBalance);
                    accountReader.Close();

                    currentBalance += amount;
                    if (currentBalance < 0)
                    {
                        Console.WriteLine("The remaining amount is not enough to take withdrawal !");
                        return false;
                    }
                    var stringCmdUpdateAccount = $"update `useraccount` set balance = {currentBalance} where AccountNumber = '{receiverAccountNumber}'";
                    var cmdUpdateAccount = new MySqlCommand(stringCmdUpdateAccount,cnn);
                    cmdUpdateAccount.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                
                // 2.3 Lưu transaction history
                var trasTransaction = new Transaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = account.AccountNumber,
                    ReceiverAccountNumber = receiverAccountNumber,
                    Type = TransactionType.DEDPOSIT,
                    Amount = amount,
                    Fee = 0,
                    Message = "Transfer : " + amount,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    TransactionStatus = TransactionStatus.DONE
                };
                var stringCmdInsertTransaction = "INSERT INTO `transaction_history`(`ID`,`TransactionCode`, `SenderAccountNumber`, `ReceiverAccountNumber`, `Type`, `Amount`, `Fee`, `Message`, `CreateAt`, `UpdateAt`, `TransactionStatus`) " +
                                                 $"VALUES ('NULL','{trasTransaction.TransactionCode}','{trasTransaction.SenderAccountNumber}','{trasTransaction.ReceiverAccountNumber}','{trasTransaction.Type}','{trasTransaction.Amount}','{trasTransaction.Fee}','{trasTransaction.Message}','{trasTransaction.CreateAt.ToString("yyyy-MM-dd HH:mm:ss")}','{trasTransaction.UpdateAt.ToString("yyyy-MM-dd HH:mm:ss")}','{(int) trasTransaction.TransactionStatus}')";
                // 3.1 Commit nếu tất cả đều thành công
                var cmdInsertTransaction = new MySqlCommand(stringCmdInsertTransaction,cnn);
                cmdInsertTransaction.ExecuteNonQuery();
                
                transaction.Commit();
                cnn.Close();
                return true;
            }
            // 3.2 Rollback nếu có sự cố
            catch (Exception e)
            {
                transaction.Rollback();
                throw;
            }
            // 2.2 Update số dư tài khoản
            return false;
        }

        public double BalanceInquiry(Account account)
        {
            
            // 1. Kết nối database
            var cnn = ConnectionHelper.getConnection();
            cnn.Open();
            // 2. Tạo transaction
            // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
            var stringCmdGetAccount = $"SELECT balance FROM `useraccount` WHERE AccountNumber = '{account.AccountNumber}' AND Status = {(int) Status.ACTIVE}";
            var cmdGetAccount = new MySqlCommand(stringCmdGetAccount,cnn);
            var accountReader = cmdGetAccount.ExecuteReader();
            if (!accountReader.Read())
            {
                accountReader.Close(); // nó ko đóng ở đây. trong trường hợp lỗi. thử account khác đi. va
                //rollback
                throw new Exception("Account is not found or has been deleted!");
            }

            // Console.WriteLine("Okie account");
            var currentBalance = accountReader.GetDouble("balance");
            // Console.WriteLine(currentBalance);
            accountReader.Close();
            cnn.Close();
            return currentBalance;
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
                var stringCmdGetAccount = $"UPDATE `useraccount` SET `Fullname` = '{account.Fullname}', `Email` = '{account.Email}', `PhoneNumber` = '{account.PhoneNumber}' WHERE `AccountNumber` = '{account.AccountNumber}';";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount,cnn);
                cmdGetAccount.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangePassword(Account account,string newPass)
        {
            try
            {
                // 1. Kết nối database
                var cnn = ConnectionHelper.getConnection();
                cnn.Open();
                // 2. Tạo transaction
                
                // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
                var stringCmdGetAccount = $"UPDATE `useraccount` SET `PasswordHash` = '{account.PasswordHash}', `Salt` = '{account.Salt}' WHERE `AccountNumber` = '{account.AccountNumber}';";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount,cnn);
                cmdGetAccount.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Transaction> transactionHisory(Account account)
        {
            try
            {
                // 1. Kết nối database
                var cnn = ConnectionHelper.getConnection();
                cnn.Open();
                // 2. Tạo transaction
                // 2.1 Lấy thông tin mới nhất của tài khoản - > select lại thôn tin
                var stringCmdGetAccount = $"Select * from `transaction_history` WHERE `SenderAccountNumber` = '{account.AccountNumber}' OR `ReceiverAccountNumber` = '{account.AccountNumber}'";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount,cnn);
                var reader = cmdGetAccount.ExecuteReader();
                List<Transaction> list = new List<Transaction>();
                while(reader.Read())
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
                            CreateAt = (DateTime) reader.GetMySqlDateTime("createAt"),
                            UpdateAt = (DateTime) reader.GetMySqlDateTime("updateAt"),
                            TransactionStatus =(TransactionStatus) reader.GetInt32("TransactionStatus"),
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
    }
}