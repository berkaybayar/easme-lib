using System.Data;
using EasMe.Extensions;
using Microsoft.Data.SqlClient;

namespace EasMe;

/// <summary>
///     SQL helper, used to execute SQL queries, and get data from SQL database.
/// </summary>
public class EasQL
{
    private readonly string _connection;

    public EasQL(string connection) {
        if (!connection.IsValidConnectionString())
            throw new InvalidDataException("EasQL given connection string is not valid");
        _connection = connection;
    }


    /// <summary>
    ///     Executes SQL query and returns DataTable.
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public DataTable GetTable(SqlCommand cmd, int timeout = 0) {
        return GetTable(_connection, cmd, timeout);
    }

    /// <summary>
    ///     Exectues SQL query and returns affected row count.
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="Timeout"></param>
    /// <returns></returns>
    public int ExecNonQuery(SqlCommand cmd, int Timeout = 0) {
        return ExecNonQuery(_connection, cmd, Timeout);
    }

    public async Task<int> ExecNonQueryAsync(SqlCommand cmd, int timeout = 0) {
        return await Task.Run(() => { return ExecNonQuery(_connection, cmd, timeout); });
    }

    /// <summary>
    ///     Exectues SQL query and returns the first column of first row in the result set returned by query. Additional
    ///     columns or rows ignored.
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public object ExecScalar(SqlCommand cmd, int timeout = 0) {
        return ExecScalar(_connection, cmd, timeout);
    }

    public async Task<object> ExecScalarAsync(SqlCommand cmd, int timeout = 0) {
        return await Task.Run(() => { return ExecScalar(_connection, cmd, timeout); });
    }

    /// <summary>
    ///     Executes a SQL query to backup database to the given folder path.
    /// </summary>
    /// <param name="backupPath"></param>
    /// <param name="timeout"></param>
    public void BackupDatabase(string backupPath, string databaseName, int timeout = 0) {
        BackupDatabase(_connection,databaseName, backupPath, timeout);
    }

    public async Task BackupDatabaseAsync(string backupPath, string databaseName,int timeout = 0) {
        await Task.Run(() => { BackupDatabase(backupPath, databaseName, timeout); });
    }

    /// <summary>
    ///     Executes a SQL query to shrink your database and SQL log data. This action will not lose you any real data but
    ///     still you should backup first.
    /// </summary>
    /// <param name="databaseLogName"></param>
    public void ShrinkDatabase(string databaseLogName = "_log") {
        ShrinkDatabase(_connection, databaseLogName);
    }

    public async Task ShrinkDatabaseAsync(string databaseLogName = "_log") {
        await Task.Run(() => { ShrinkDatabase(databaseLogName); });
    }


    /// <summary>
    ///     Deletes all records in given table but keeps the table. This action can not be undone, be aware of the risks before
    ///     running this.
    /// </summary>
    /// <param name="tableName"></param>
    public void TruncateTable(string tableName) {
        TruncateTable(_connection, tableName);
    }

    public async Task TruncateTableAsync(string tableName) {
        await Task.Run(() => { TruncateTable(tableName); });
    }


    /// <summary>
    ///     Deletes all records in the table and the table from database. This action can not be undone, be aware of the risks
    ///     before running this.
    /// </summary>
    /// <param name="tableName"></param>
    public void DropTable(string tableName) {
        DropTable(_connection, tableName);
    }

    public async Task DropTableAsync(string tableName) {
        await Task.Run(() => { DropTable(tableName); });
    }

    /// <summary>
    ///     Deletes all records and all tables and the database entirely. This action can not be undone, be aware of the risks
    ///     before running this.
    /// </summary>
    /// <param name="databaseName"></param>
    public void DropDatabase(string databaseName) {
        DropDatabase(_connection, databaseName);
    }

    /// <summary>
    ///     Gets all table names in SQL database and returns.
    /// </summary>
    /// <returns></returns>
    public List<string> GetAllTableName() {
        return GetAllTableName(_connection);
    }


    #region EasQL Static Methods

    /// <summary>
    ///     Executes SQL query and returns DataTable.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="cmd"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static DataTable GetTable(string connection, SqlCommand cmd, int timeout = 0) {
        DataTable dt = new();
        using var conn = new SqlConnection(connection);
        conn.Open();
        cmd.Connection = conn;
        var da = new SqlDataAdapter(cmd);
        da.SelectCommand.CommandTimeout = timeout;
        da.Fill(dt);
        return dt;
    }

    public static async Task<DataTable> GetTableAsync(string connection, SqlCommand cmd, int timeout = 0) {
        return await Task.Run(() => { return GetTable(connection, cmd, timeout); });
    }

    /// <summary>
    ///     Exectues SQL query and returns affected row count.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="cmd"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static int ExecNonQuery(string connection, SqlCommand cmd, int timeout = 0) {
        using SqlConnection conn = new(connection);
        cmd.Connection = conn;
        cmd.CommandTimeout = timeout;
        conn.Open();
        return cmd.ExecuteNonQuery();
    }

    public static async Task<int> ExecNonQueryAsync(string connection, SqlCommand cmd, int timeout = 0) {
        return await Task.Run(() => ExecNonQuery(connection, cmd, timeout));
    }

    /// <summary>
    ///     Exectues SQL query and returns the first column of first row in the result set returned by query. Additional
    ///     columns or rows ignored.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="cmd"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static object ExecScalar(string connection, SqlCommand cmd, int timeout = 0) {
        using SqlConnection conn = new(connection);
        cmd.Connection = conn;
        cmd.CommandTimeout = timeout;
        conn.Open();
        return cmd.ExecuteScalar();
    }

    public static async Task<object> ExecScalarAsync(string connection, SqlCommand cmd, int timeout = 0) {
        return await Task.Run(() => ExecScalar(connection, cmd, timeout));
    }

    public static void BackupDatabase(string connection, string databaseName ,string backupFolderPath, int timeout = 0) {
        if (!Directory.Exists(backupFolderPath)) Directory.CreateDirectory(backupFolderPath);
        var bkPath = backupFolderPath + "\\bk_" + databaseName + ".bak";
        var query = $@"BACKUP DATABASE {databaseName} TO DISK = '{bkPath}'";
        var cmd = new SqlCommand(query);
        ExecNonQuery(connection, cmd, timeout);
    }

    public static async Task BackupDatabaseAsync(string connection, string databaseName,string backupFolderPath, int timeout = 0) {
        await Task.Run(() => { BackupDatabase(connection, databaseName, backupFolderPath, timeout); });
    }

    public static void ShrinkDatabase(string connection, string databaseName,string databaseLogName = "_log") {
        if (databaseLogName == "_log") databaseLogName = databaseName + databaseLogName;
        var query = $@"BEGIN
                                ALTER DATABASE [{databaseName}] SET RECOVERY SIMPLE WITH NO_WAIT
                                DBCC SHRINKFILE(N'{databaseLogName}', 1)
                                ALTER DATABASE [{databaseName}] SET RECOVERY FULL WITH NO_WAIT
                            END
                            BEGIN
                                ALTER DATABASE [{databaseName}] SET RECOVERY SIMPLE WITH NO_WAIT
                                DBCC SHRINKFILE(N'{databaseName}', 1)
                                ALTER DATABASE [{databaseName}] SET RECOVERY FULL WITH NO_WAIT
                            END";
        var cmd = new SqlCommand(query);
        ExecNonQuery(connection, cmd);
    }

    public static async Task ShrinkDatabaseAsync(string connection, string databaseLogName = "_log") {
        await Task.Run(() => { ShrinkDatabase(connection, databaseLogName); });
    }


    public static void TruncateTable(string connection, string tableName) {
        var query = $@"TRUNCATE TABLE {tableName}";
        var cmd = new SqlCommand(query);
        ExecNonQuery(connection, cmd);
    }

    public static async Task TruncateTableAsync(string connection, string tableName) {
        await Task.Run(() => { TruncateTable(connection, tableName); });
    }

    /// <summary>
    ///     Deletes all records in the table and the table from database. This action can not be undone, be aware of the risks
    ///     before running this.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="tableName"></param>
    public static void DropTable(string connection, string tableName) {
        var query = $@"DROP TABLE {tableName}";
        var cmd = new SqlCommand(query);
        ExecNonQuery(connection, cmd);
    }

    public static async Task DropTableAsync(string connection, string tableName) {
        await Task.Run(() => { DropTable(connection, tableName); });
    }

    /// <summary>
    ///     Deletes all records and all tables and the database entirely. This action can not be undone, be aware of the risks
    ///     before running this.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="databaseName"></param>
    public static void DropDatabase(string connection, string databaseName) {
        var query = $@"DROP DATABASE {databaseName}";
        var cmd = new SqlCommand(query);
        ExecNonQuery(connection, cmd);
    }


    /// <summary>
    ///     Gets all table names in SQL database and returns.
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    public static List<string> GetAllTableName(string connection) {
        var query = @"SELECT '['+SCHEMA_NAME(schema_id)+'].['+name+']' FROM sys.tables";
        var list = new List<string>();
        var cmd = new SqlCommand(query);
        var dt = GetTable(connection, cmd);
        foreach (DataRow row in dt.Rows) list.Add(row[0].ToString());
        return list;
    }

    public static List<string> GetColumns(string connection, string tableName) {
        var list = new List<string>();
        var query = $@"SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('{tableName}')";
        SqlCommand cmd = new(query);
        var dt = GetTable(connection, cmd);
        foreach (DataRow row in dt.Rows)
            if (row != null) {
                var columnName = row[0].ToString();
                if (!string.IsNullOrEmpty(columnName)) list.Add(columnName);
            }

        return list;
    }

    #endregion
}