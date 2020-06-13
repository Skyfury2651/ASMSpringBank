using MySql.Data.MySqlClient;

namespace ConsoleApplication1.Helper
{
    public class ConnectionHelper
    {
        private const string DataBaseServer = "127.0.0.1";
        private const string DatabaseName = "t1908e_project_asm";
        private const string DatabasePort = "3306";
        private const string DataBaseUser = "root";
        private const string DataBasePass = "";
        private static MySqlConnection _connection;

        public static MySqlConnection getConnection()
        {
            if (_connection == null)
            {
               _connection = new MySqlConnection($"SERVER={DataBaseServer};DATABASE={DatabaseName};user={DataBaseUser};port={DatabasePort};password={DataBasePass}");
            }
            
            return _connection;
        }
    }
}