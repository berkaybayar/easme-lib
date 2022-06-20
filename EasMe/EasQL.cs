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
        /// <summary>
        /// Executes SQL query and returns DataTable.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="cmd"></param>
        /// <param name="Timeout"></param>
        /// <returns>DataTable</returns>
        public DataTable GetTable(string Connection, SqlCommand cmd, int Timeout = 0)
        {
            DataTable dt = new();
            using (SqlConnection conn = new SqlConnection(Connection))
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand.CommandTimeout = Timeout;
                    da.Fill(dt);
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception e)
                {
                    throw new EasException(Error.SQL_FAILED_GET_TABLE, cmd.CommandText, e);

                }

            }
            return dt;
        }
        public DataTable GetTable(SqlCommand cmd, int Timeout = 0)
        {
            if (string.IsNullOrEmpty(_connection)) throw new EasException(Error.SQL_CONNECTION_NOT_INITIALIZED);
            return GetTable(_connection, cmd, Timeout);
        }
        public int ExecNonQuery(string Connection, SqlCommand cmd, int Timeout = 0)
        {
            int rowsEffected = 0;
            using (SqlConnection conn = new SqlConnection(Connection))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = Timeout;
                    conn.Open();
                    rowsEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception e)
                {
                    throw new EasException(Error.SQL_FAILED_EXEC_NONQUERY, cmd.CommandText, e);

                }

            }
            return rowsEffected;
        }
        public int ExecNonQuery(SqlCommand cmd, int Timeout = 0)
        {
            if (string.IsNullOrEmpty(_connection)) throw new EasException(Error.SQL_CONNECTION_NOT_INITIALIZED);
            return ExecNonQuery(_connection, cmd, Timeout);
        }

        public object ExecScalar(string Connection, SqlCommand cmd, int Timeout = 0)
        {
            var obj = new object();
            using (SqlConnection conn = new SqlConnection(Connection))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = Timeout;
                    conn.Open();
                    obj = cmd.ExecuteScalar();
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception e)
                {
                    throw new EasException(Error.SQL_FAILED_EXEC_SCALAR, cmd.CommandText, e);

                }


            }
            return obj;
        }
        public object ExecScalar(SqlCommand cmd, int Timeout = 0)
        {
            if (string.IsNullOrEmpty(_connection)) throw new EasException(Error.SQL_CONNECTION_NOT_INITIALIZED);
            return ExecScalar(_connection, cmd, Timeout);
        }

        public void BackupDatabase(string Connection, string DatabaseName, string BackupPath, int Timeout = 0)
        {
            try
            {
                string query = $@"BACKUP DATABASE {DatabaseName} TO DISK = '{BackupPath}\{DatabaseName}-{DateTime.Now.ToString(" H-mm-ss dd-MM-yyyy")}.bak'";
                var cmd = new SqlCommand(query);
                ExecNonQuery(Connection, cmd, Timeout);
            }
            catch (Exception e)
            {
                throw new EasException(Error.SQL_FAILED_BACKUP_DATABASE, DatabaseName, e);
            }
        }
        public void ShrinkDatabase(string Connection, string DatabaseName, string DatabaseLogName = "_log")
        {
            try
            {
                if (DatabaseLogName == "_log") DatabaseLogName = DatabaseName + DatabaseLogName;
                string query = $@"BEGIN
                                ALTER DATABASE [{DatabaseName}] SET RECOVERY SIMPLE WITH NO_WAIT
                                DBCC SHRINKFILE(N'{DatabaseLogName}', 1)
                                ALTER DATABASE [{DatabaseName}] SET RECOVERY FULL WITH NO_WAIT
                            END
                            BEGIN
                                ALTER DATABASE [{DatabaseName}] SET RECOVERY SIMPLE WITH NO_WAIT
                                DBCC SHRINKFILE(N'{DatabaseName}', 1)
                                ALTER DATABASE [{DatabaseName}] SET RECOVERY FULL WITH NO_WAIT
                            END";
                var cmd = new SqlCommand(query);
                ExecNonQuery(Connection, cmd);
            }
            catch (Exception e)
            {
                throw new EasException(Error.SQL_FAILED_SHRINK_DATABASE, DatabaseName, e);
            }
        }


        public void TruncateTable(string Connection, string TableName)
        {
            
            try
            {
                string query = $@"TRUNCATE TABLE {TableName}";
                var cmd = new SqlCommand(query);
                ExecNonQuery(Connection, cmd);
            }
            catch (Exception e)
            {
                throw new EasException(Error.SQL_TABLE_TRUNCATE_FAILED, TableName, e);
            }
        }

        public void DropTable(string Connection, string TableName)
        {
            try
            {
                string query = $@"DROP TABLE {TableName}";
                var cmd = new SqlCommand(query);
                ExecNonQuery(Connection, cmd);
            }
            catch (Exception e)
            {
                throw new EasException(Error.SQL_FAILED_DROP_TABLE, TableName, e);
            }
        }
        public void DropDatabase(string Connection, string DatabaseName)
        {
            try
            {
                string query = $@"DROP DATABASE {DatabaseName}";
                var cmd = new SqlCommand(query);
                ExecNonQuery(Connection, cmd);
            }
            catch (Exception e)
            {
                throw new EasException(Error.SQL_FAILED_DROP_DATABASE, DatabaseName, e);
            }
        }

        public List<string> GetAllTableName(string Connection)
        {
            try
            {
                string query = $@"SELECT '['+SCHEMA_NAME(schema_id)+'].['+name+']' FROM sys.tables";
                var list = new List<string>();
                SqlCommand cmd = new SqlCommand(query);
                var dt = GetTable(Connection, cmd);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row[0].ToString());
                }
                return list;
            }
            catch (Exception e)
            {
                throw new EasException(Error.SQL_FAILED_GET_ALL_TABLE_NAME,Connection, e);
            }
        }



    }

}
