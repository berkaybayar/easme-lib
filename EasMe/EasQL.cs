using System.Data;
using System.Data.SqlClient;


namespace EasMe
{
    public class EasQL
    {

        public DataTable GetTable(string Connection, SqlCommand cmd, int Timeout = 0)
        {
            DataTable dt = new DataTable();
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
                catch (Exception) { throw; }

            }
            return dt;
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
                catch (Exception) { throw; }

            }
            return rowsEffected;
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
                catch (Exception) { throw; }


            }
            return obj;
        }

        public void BackupDatabase(string Connection, string DatabaseName, string BackupPath, int Timeout = 0)
        {
            string query = $@"BACKUP DATABASE {DatabaseName} TO DISK = '{BackupPath}\{DatabaseName}-{DateTime.Now.ToString(" H-mm-ss dd-MM-yyyy")}.bak'";
            var cmd = new SqlCommand(query);
            ExecNonQuery(Connection, cmd, Timeout);
        }
        public void ShrinkDatabase(string Connection, string DatabaseName, string DatabaseLogName = "_log")
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


        public void TruncateTable(string Connection, string TableName)
        {
            string query = $@"TRUNCATE TABLE {TableName}";
            var cmd = new SqlCommand(query);
            ExecNonQuery(Connection, cmd);
        }

        public void DropTable(string Connection, string TableName)
        {
            string query = $@"DROP TABLE {TableName}";
            var cmd = new SqlCommand(query);
            ExecNonQuery(Connection, cmd);
        }
        public void DropDatabase(string Connection, string DatabaseName)
        {
            string query = $@"DROP DATABASE {DatabaseName}";
            var cmd = new SqlCommand(query);
            ExecNonQuery(Connection, cmd);
        }

        public List<string> GetAllTableName(string Connection)
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



    }

}
