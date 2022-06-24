using System.Data;
using System.Data.SqlClient;

namespace EasMe
{
    /// <summary>
    /// SQL helper, used to execute SQL queries, and get data from SQL database.
    /// </summary>
    public static class EasQL
    {
        private static string Connection { get; set; }
        
        /// <summary>
        /// Executes SQL query and returns DataTable.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="cmd"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static DataTable GetTable(string Connection, SqlCommand cmd, int Timeout = 0)
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
                    throw new EasException(Error.SQL_GET_TABLE_FAILED, "SQL Query: " + cmd.CommandText, e);
                }

            }
            return dt;
        }

        /// <summary>
        /// Exectues SQL query and returns affected row count.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="cmd"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static int ExecNonQuery(string Connection, SqlCommand cmd, int Timeout = 0)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new(Connection))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = Timeout;
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception e)
                {
                    throw new EasException(Error.SQL_EXEC_NON_QUERY_FAILED, "SQL Query: " + cmd.CommandText, e);

                }

            }
            return rowsAffected;
        }
        /// <summary>
        /// Exectues SQL query and returns the first column of first row in the result set returned by query. Additional columns or rows ignored.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="cmd"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static object ExecScalar(string Connection, SqlCommand cmd, int Timeout = 0)
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
                    throw new EasException(Error.SQL_EXEC_NON_QUERY_FAILED, "SQL Query: " + cmd.CommandText, e);

                }
            }
            return obj;
        }

        /// <summary>
        /// Executes a SQL query to backup database to the given folder path.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="DatabaseName"></param>
        /// <param name="BackupFolderPath"></param>
        /// <param name="Timeout"></param>
        /// <exception cref="EasException"></exception>
        public static void BackupDatabase(string Connection, string DatabaseName, string BackupFolderPath, int Timeout = 0)
        {
            try
            {
                string query = $@"BACKUP DATABASE {DatabaseName} TO DISK = '{BackupFolderPath}\{DatabaseName}-{DateTime.Now: H-mm-ss dd-MM-yyyy}.bak'";
                var cmd = new SqlCommand(query);
                ExecNonQuery(Connection, cmd, Timeout);
            }
            catch (Exception e)
            {
                throw new EasException(Error.SQL_BACKUP_DATABASE_FAILED, DatabaseName, e);
            }
        }
        /// <summary>
        /// Executes a SQL query to shrink your database and SQL log data. This action will not lose you any real data but still you should backup first.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="DatabaseName"></param>
        /// <param name="DatabaseLogName"></param>
        /// <exception cref="EasException"></exception>
        public static void ShrinkDatabase(string Connection, string DatabaseName, string DatabaseLogName = "_log")
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
                throw new EasException(Error.SQL_SHRINK_DATABASE_FAILED, DatabaseName, e);
            }
        }
        /// <summary>
        /// Deletes all records in given table but keeps the table. This action can not be undone, be aware of the risks before running this.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="TableName"></param>
        /// <exception cref="EasException"></exception>
        public static void TruncateTable(string Connection, string TableName)
        {

            try
            {
                string query = $@"TRUNCATE TABLE {TableName}";
                var cmd = new SqlCommand(query);
                ExecNonQuery(Connection, cmd);
            }
            catch (Exception e)
            {
                throw new EasException(Error.SQL_TRUNCATE_FAILED, TableName, e);
            }
        }
        /// <summary>
        /// Deletes all records in the table and the table from database. This action can not be undone, be aware of the risks before running this.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="TableName"></param>
        /// <exception cref="EasException"></exception>
        public static void DropTable(string Connection, string TableName)
        {
            try
            {
                string query = $@"DROP TABLE {TableName}";
                var cmd = new SqlCommand(query);
                ExecNonQuery(Connection, cmd);
            }
            catch (Exception e)
            {
                throw new EasException(Error.SQL_DROP_TABLE_FAILED, TableName, e);
            }
        }
        /// <summary>
        /// Deletes all records and all tables and the database entirely. This action can not be undone, be aware of the risks before running this.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="DatabaseName"></param>
        /// <exception cref="EasException"></exception>
        public static void DropDatabase(string Connection, string DatabaseName)
        {
            try
            {
                string query = $@"DROP DATABASE {DatabaseName}";
                var cmd = new SqlCommand(query);
                ExecNonQuery(Connection, cmd);
            }
            catch (Exception e)
            {
                throw new EasException(Error.SQL_DROP_DATABASE_FAILED, DatabaseName, e);
            }
        }
        /// <summary>
        /// Gets all table names in SQL database and returns.
        /// </summary>
        /// <param name="Connection"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static List<string> GetAllTableName(string Connection)
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
                throw new EasException(Error.SQL_ERROR, "", e);
            }
        }




        /// <summary>
        /// Loads connection string to use functions without giving connection string each time.
        /// </summary>
        /// <param name="connectionString"></param>
        public static void LoadConnectionString(string connectionString)
        {
            Connection = connectionString;
        }

        private static void CheckIfLoaded()
        {
            if (string.IsNullOrEmpty(Connection)) throw new EasException(Error.SQL_CONNECTION_NOT_INITIALIZED, "Can not use functions without connection string parameter if you have not used Load() function to load connection string.");
        }

        /// <summary>
        /// Executes SQL query and returns DataTable.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="cmd"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static DataTable GetTable(SqlCommand cmd, int Timeout = 0)
        {
            CheckIfLoaded();
            if (string.IsNullOrEmpty(Connection)) throw new EasException(Error.SQL_CONNECTION_NOT_INITIALIZED);
            return GetTable(Connection, cmd, Timeout);
        }
        /// <summary>
        /// Exectues SQL query and returns affected row count.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="cmd"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static int ExecNonQuery(SqlCommand cmd, int Timeout = 0)
        {
            CheckIfLoaded();
            if (string.IsNullOrEmpty(Connection)) throw new EasException(Error.SQL_CONNECTION_NOT_INITIALIZED);
            return ExecNonQuery(Connection, cmd, Timeout);
        }

        /// <summary>
        /// Exectues SQL query and returns the first column of first row in the result set returned by query. Additional columns or rows ignored.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="cmd"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static object ExecScalar(SqlCommand cmd, int Timeout = 0)
        {
            CheckIfLoaded();
            if (string.IsNullOrEmpty(Connection)) throw new EasException(Error.SQL_CONNECTION_NOT_INITIALIZED);
            return ExecScalar(Connection, cmd, Timeout);
        }

        /// <summary>
        /// Executes a SQL query to backup database to the given folder path.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="DatabaseName"></param>
        /// <param name="BackupFolderPath"></param>
        /// <param name="Timeout"></param>
        /// <exception cref="EasException"></exception>
        public static void BackupDatabase(string DatabaseName, string BackupPath, int Timeout = 0)
        {
            CheckIfLoaded();
            BackupDatabase(Connection, DatabaseName, BackupPath, Timeout);
        }
        /// <summary>
        /// Executes a SQL query to shrink your database and SQL log data. This action will not lose you any real data but still you should backup first.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="DatabaseName"></param>
        /// <param name="DatabaseLogName"></param>
        /// <exception cref="EasException"></exception>
        public static void ShrinkDatabase(string DatabaseName, string DatabaseLogName = "_log")
        {
            CheckIfLoaded();
            ShrinkDatabase(Connection, DatabaseName, DatabaseLogName);
        }



        /// <summary>
        /// Deletes all records in given table but keeps the table. This action can not be undone, be aware of the risks before running this.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="TableName"></param>
        /// <exception cref="EasException"></exception>
        public static void TruncateTable(string TableName)
        {
            CheckIfLoaded();
            TruncateTable(Connection, TableName);
        }



        /// <summary>
        /// Deletes all records in the table and the table from database. This action can not be undone, be aware of the risks before running this.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="TableName"></param>
        /// <exception cref="EasException"></exception>
        public static void DropTable(string TableName)
        {
            CheckIfLoaded();
            DropTable(Connection, TableName);
        }


        /// <summary>
        /// Deletes all records and all tables and the database entirely. This action can not be undone, be aware of the risks before running this.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="DatabaseName"></param>
        /// <exception cref="EasException"></exception>
        public static void DropDatabase(string DatabaseName)
        {
            CheckIfLoaded();
            DropDatabase(Connection, DatabaseName);
        }

        /// <summary>
        /// Gets all table names in SQL database and returns.
        /// </summary>
        /// <param name="Connection"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static List<string> GetAllTableName()
        {
            CheckIfLoaded();
            return GetAllTableName(Connection);
        }
    }
}
