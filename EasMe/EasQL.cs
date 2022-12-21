using EasMe.Exceptions;
using EasMe.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading;

namespace EasMe
{
    /// <summary>
    /// SQL helper, used to execute SQL queries, and get data from SQL database.
    /// </summary>
    public class EasQL
    {
        private static string? Connection { get; set; }

        public EasQL(string connection)
        {
            if (!connection.IsValidConnectionString()) throw new NotValidException("EasQL given connection string is not valid");
            Connection = connection;
        }
        /// <summary>
        /// Executes SQL query and returns DataTable.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="cmd"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public DataTable GetTable(SqlCommand cmd, int Timeout = 0)
        {
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
        public int ExecNonQuery(SqlCommand cmd, int Timeout = 0)
        {
            return ExecNonQuery(Connection, cmd, Timeout);
        }

        public async Task<int> ExecNonQueryAsync(SqlCommand cmd, int timeout = 0)
        {
            return await Task.Run(() =>
            {
                return ExecNonQuery(Connection, cmd, timeout);
            });
		}
        /// <summary>
        /// Exectues SQL query and returns the first column of first row in the result set returned by query. Additional columns or rows ignored.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="cmd"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public object ExecScalar(SqlCommand cmd, int Timeout = 0)
        {
            return ExecScalar(Connection, cmd, Timeout);
        }
		public async Task<object> ExecScalarAsync(SqlCommand cmd, int timeout = 0)
		{
			return await Task.Run(() =>
			{
				return ExecScalar(Connection, cmd, timeout);
			});
		}
		/// <summary>
		/// Executes a SQL query to backup database to the given folder path.
		/// </summary>
		/// <param name="Connection"></param>
		/// <param name="DatabaseName"></param>
		/// <param name="BackupFolderPath"></param>
		/// <param name="Timeout"></param>
		/// <exception cref="EasException"></exception>
		public void BackupDatabase(string BackupPath, int Timeout = 0)
        {
            BackupDatabase(Connection, BackupPath, Timeout);
        }

		public async Task BackupDatabaseAsync(string BackupPath, int Timeout = 0)
		{
			await Task.Run(() =>
			{
			    BackupDatabase(BackupPath,Timeout);
			});
		}
        
		/// <summary>
		/// Executes a SQL query to shrink your database and SQL log data. This action will not lose you any real data but still you should backup first.
		/// </summary>
		/// <param name="Connection"></param>
		/// <param name="DatabaseName"></param>
		/// <param name="DatabaseLogName"></param>
		/// <exception cref="EasException"></exception>
		public void ShrinkDatabase(string DatabaseLogName = "_log")
        {
            ShrinkDatabase(Connection, DatabaseLogName);
        }
		public async Task ShrinkDatabaseAsync(string DatabaseLogName = "_log")
		{
			await Task.Run(() =>
            {
				ShrinkDatabase( DatabaseLogName);
			});
		}


		/// <summary>
		/// Deletes all records in given table but keeps the table. This action can not be undone, be aware of the risks before running this.
		/// </summary>
		/// <param name="Connection"></param>
		/// <param name="TableName"></param>
		/// <exception cref="EasException"></exception>
		public void TruncateTable(string TableName)
        {
            TruncateTable(Connection, TableName);
        }
		public async Task TruncateTableAsync(string TableName)
		{
			await Task.Run(() =>
			{
				TruncateTable(TableName);
			});
		}


		/// <summary>
		/// Deletes all records in the table and the table from database. This action can not be undone, be aware of the risks before running this.
		/// </summary>
		/// <param name="Connection"></param>
		/// <param name="TableName"></param>
		/// <exception cref="EasException"></exception>
		public void DropTable(string TableName)
        {
            DropTable(Connection, TableName);
        }

		public async Task DropTableAsync(string TableName)
		{
			await Task.Run(() =>
			{
				DropTable(TableName);
			});
		}
		/// <summary>
		/// Deletes all records and all tables and the database entirely. This action can not be undone, be aware of the risks before running this.
		/// </summary>
		/// <param name="Connection"></param>
		/// <param name="DatabaseName"></param>
		/// <exception cref="EasException"></exception>
		public void DropDatabase(string DatabaseName)
        {
            DropDatabase(Connection, DatabaseName);
        }

        /// <summary>
        /// Gets all table names in SQL database and returns.
        /// </summary>
        /// <param name="Connection"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public List<string> GetAllTableName()
        {
            return GetAllTableName(Connection);
        }


        #region EasQL Static Methods
        /// <summary>
        /// Executes SQL query and returns DataTable.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmd"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static DataTable GetTable(string connection, SqlCommand cmd, int timeout = 0)
        {
            DataTable dt = new();
            using SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = timeout;
            da.Fill(dt);
            return dt;
        }

		public static async Task<DataTable> GetTableAsync(string connection, SqlCommand cmd, int timeout = 0)
		{
			return await Task<DataTable>.Run(() =>
			{
				return EasQL.GetTable(connection, cmd, timeout);
			});
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
            using SqlConnection conn = new(Connection);
            cmd.Connection = conn;
            cmd.CommandTimeout = Timeout;
            conn.Open();
            return cmd.ExecuteNonQuery();
        }

		public static async Task<int> ExecNonQueryAsync(string connection, SqlCommand cmd, int timeout = 0)
		{
			return await Task<int>.Run(() =>
			{
				return EasQL.ExecNonQuery(connection, cmd, timeout);
			});
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
            using SqlConnection conn = new(Connection);
            cmd.Connection = conn;
            cmd.CommandTimeout = Timeout;
            conn.Open();
            return cmd.ExecuteScalar();
        }
		public static async Task<object> ExecScalarAsync(string connection, SqlCommand cmd, int timeout = 0)
		{
			return await Task<object>.Run(() =>
			{
				return EasQL.ExecScalar(connection, cmd, timeout);
			});
		}

		public static void BackupDatabase(string connection, string backupFolderPath, int timeout = 0)
        {
            var dbName = connection.ParseDatabaseName();
            if (!Directory.Exists(backupFolderPath)) Directory.CreateDirectory(backupFolderPath);
            var bkPath = backupFolderPath + "\\bk_" + dbName + ".bak";
            string query = $@"BACKUP DATABASE {dbName} TO DISK = '{bkPath}'";
            var cmd = new SqlCommand(query);
            ExecNonQuery(connection, cmd, timeout);
        }

		public static async Task BackupDatabaseAsync(string connection, string backupFolderPath, int timeout = 0)
		{
		    await Task.Run(() =>
			{
				EasQL.BackupDatabase(connection, backupFolderPath, timeout);
			});
		}

		public static void ShrinkDatabase(string Connection, string DatabaseLogName = "_log")
        {
            var DatabaseName = Connection.ParseDatabaseName();
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
		public static async Task ShrinkDatabaseAsync(string Connection, string DatabaseLogName = "_log")
		{
			await Task.Run(() =>
			{
				EasQL.ShrinkDatabase(Connection, DatabaseLogName);
			});
		}

		
		public static void TruncateTable(string Connection, string TableName)
        {
            string query = $@"TRUNCATE TABLE {TableName}";
            var cmd = new SqlCommand(query);
            ExecNonQuery(Connection, cmd);
        }

		public static async Task TruncateTableAsync(string Connection, string TableName)
		{
			await Task.Run(() =>
			{
				EasQL.TruncateTable(Connection, TableName);
			});
		}
		/// <summary>
		/// Deletes all records in the table and the table from database. This action can not be undone, be aware of the risks before running this.
		/// </summary>
		/// <param name="Connection"></param>
		/// <param name="TableName"></param>
		/// <exception cref="EasException"></exception>
		public static void DropTable(string Connection, string TableName)
        {
            string query = $@"DROP TABLE {TableName}";
            var cmd = new SqlCommand(query);
            ExecNonQuery(Connection, cmd);
        }
		public static async Task DropTableAsync(string Connection, string TableName)
		{
			await Task.Run(() =>
			{
				EasQL.DropTable(Connection, TableName);
			});
		}
		/// <summary>
		/// Deletes all records and all tables and the database entirely. This action can not be undone, be aware of the risks before running this.
		/// </summary>
		/// <param name="Connection"></param>
		/// <param name="DatabaseName"></param>
		/// <exception cref="EasException"></exception>
		public static void DropDatabase(string Connection, string DatabaseName)
        {
            string query = $@"DROP DATABASE {DatabaseName}";
            var cmd = new SqlCommand(query);
            ExecNonQuery(Connection, cmd);
        }

		

		/// <summary>
		/// Gets all table names in SQL database and returns.
		/// </summary>
		/// <param name="Connection"></param>
		/// <returns></returns>
		/// <exception cref="EasException"></exception>
		public static List<string> GetAllTableName(string Connection)
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

        public static List<string> GetColumns(string connection, string tableName)
        {
            var list = new List<string>();
            string query = $@"SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('{tableName}')";
            SqlCommand cmd = new(query);
            var dt = GetTable(connection, cmd);
            foreach (DataRow row in dt.Rows)
            {
                if (row != null)
                {
                    var columnName = row[0].ToString();
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        list.Add(columnName);
                    }
                }
            }
            return list;
        }
        #endregion




    }
}
