using System;
using ConsoleApplication1.Enity;
using ConsoleApplication1.Helper;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1.Model
{
    public class AccountModel
    {
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
            cmd.ExecuteNonQuery();
            cnn.Close();
            Console.WriteLine("\nRegister successful.");
            Console.WriteLine("-----------------------------");
        }

        public Account GetActiveAccountByTheUsername(String username)
        {
            Account account = null;
            var cnn = ConnectionHelper.getConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"select * from `useraccount` where Username = '{username}' and Status = {(int) Status.ACTIVE}", cnn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Console.WriteLine(reader.GetMySqlDateTime("createAt"));
                // Console.WriteLine(reader.GetMySqlDateTime("createAt").GetType());
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
                    Status = Status.ACTIVE,
                    Role = (Role) reader.GetUInt32("Role"),
                    CreatedAt = reader.GetMySqlDateTime("CreatedAt").ToString(),
                    UpdatedAt = reader.GetMySqlDateTime("UpdatedAt").ToString()
                };
            }

            reader.Close();
            cnn.Close();
            return account;
        }
    }
}