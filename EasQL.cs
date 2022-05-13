using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace EasMe
{
    public class EasQL
    {

        public DataTable GetTable(string connection, SqlCommand cmd, int timeout = 0)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand.CommandTimeout = timeout; //Default timeout is 0, if you set timeout when pulling massive data it will give you an error
                    da.Fill(dt);
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return dt;
        }

        public int ExecNonQuery(string connection, SqlCommand cmd, int timeout = 0)
        {
            int rowsEffected = 0;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = timeout; //Default timeout is 0, if you set timeout when executing massive data it will give you an error
                    conn.Open();
                    rowsEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return rowsEffected;
        }

        public object ExecScalar(string connection, SqlCommand cmd, int timeout = 0)
        {
            var obj = new object();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = timeout;
                    conn.Open();
                    obj = cmd.ExecuteScalar();
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return obj;
        }

        public int ExecStoredProcedure(string connection, SqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            return ExecNonQuery(connection, cmd);
        }

        public void BackupDatabase(string connection, string dbname, string path) //Path value must not contain new backup file name it is generated in the function
        {
            string query = $@"BACKUP DATABASE {dbname} TO DISK = '{path}\{dbname + DateTime.Now.ToString(" H-mm-ss dd-MM-yyyy")}.bak'";
            var cmd = new SqlCommand(query);
            ExecNonQuery(connection, cmd);
        }
        public void ShrinkDBandLogs(string connection, string dbname, string dblogname)
        {
            string query = $@"ALTER DATABASE [{dbname}] SET RECOVERY SIMPLE WITH NO_WAIT
                            DBCC SHRINKFILE(N'{dblogname}', 1)
                            ALTER DATABASE [{dbname}] SET RECOVERY FULL WITH NO_WAIT";
            var cmd = new SqlCommand(query);
            ExecNonQuery(connection, cmd);
            query = $@"ALTER DATABASE [{dbname}] SET RECOVERY SIMPLE WITH NO_WAIT
                            DBCC SHRINKFILE(N'{dbname}', 1)
                            ALTER DATABASE [{dbname}] SET RECOVERY FULL WITH NO_WAIT";
            cmd = new SqlCommand(query);
            ExecNonQuery(connection, cmd);
        }
       

        public int TruncateTable(string connection, string table)
        {
            string query = $@"TRUNCATE TABLE {table}";
            var cmd = new SqlCommand(query);            
            return ExecNonQuery(connection, cmd);
        }

        public int DropTable(string connection, string table)
        {
            string query = $@"DROP TABLE {table}";
            var cmd = new SqlCommand(query);            
            return ExecNonQuery(connection, cmd);
        }
        public List<string> GetAllTableName(string connection) //SCHEMANAME.TABLENAME
        {
            string query = $@"SELECT '['+SCHEMA_NAME(schema_id)+'].['+name+']' FROM sys.tables";
            var list = new List<string>();
            SqlCommand cmd = new SqlCommand(query);
            var dt = GetTable(connection, cmd);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row[0].ToString());
            }
            return list;
        }
        public int DropDatabase(string connection, string dbname)
        {
            string query = $@"DROP DATABASE {dbname}";
            var cmd = new SqlCommand(query);            
            return ExecNonQuery(connection, cmd);
        }


    }

}
