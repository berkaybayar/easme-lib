using System.Data;
using System.Data.SqlClient;


namespace EasMe
{
    /// <summary>
    /// SQL helper, used to execute SQL queries, and get data from SQL database.
    /// </summary>
    public class EasQL
    {
        private static string _connection;

        public EasQL(string connectionString)
        {
            _connection = connectionString;
        }


        public DataTable GetTable(SqlCommand cmd, int Timeout = 0)
        {
            if (string.IsNullOrEmpty(_connection)) throw new EasException(Error.SQL_CONNECTION_NOT_INITIALIZED);
            return EasQLStatic.GetTable(_connection, cmd, Timeout);
        }

        public int ExecNonQuery(SqlCommand cmd, int Timeout = 0)
        {
            if (string.IsNullOrEmpty(_connection)) throw new EasException(Error.SQL_CONNECTION_NOT_INITIALIZED);
            return EasQLStatic.ExecNonQuery(_connection, cmd, Timeout);
        }


        public object ExecScalar(SqlCommand cmd, int Timeout = 0)
        {
            if (string.IsNullOrEmpty(_connection)) throw new EasException(Error.SQL_CONNECTION_NOT_INITIALIZED);
            return EasQLStatic.ExecScalar(_connection, cmd, Timeout);
        }


        public void BackupDatabase(string DatabaseName, string BackupPath, int Timeout = 0)
        {
            EasQLStatic.BackupDatabase(_connection, DatabaseName, BackupPath, Timeout);
        }

        public void ShrinkDatabase(string DatabaseName, string DatabaseLogName = "_log")
        {
            EasQLStatic.ShrinkDatabase(_connection, DatabaseName, DatabaseLogName);
        }




        public void TruncateTable(string TableName)
        {

            EasQLStatic.TruncateTable(_connection, TableName);
        }




        public void DropTable(string TableName)
        {
            EasQLStatic.DropTable(_connection, TableName);
        }

        public void DropDatabase(string DatabaseName)
        {
            EasQLStatic.DropDatabase(_connection, DatabaseName);
        }


        public List<string> GetAllTableName()
        {
            return EasQLStatic.GetAllTableName(_connection);
        }


    }

}
