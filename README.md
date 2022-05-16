# Introduction
 Collection of classes that will save you time and help you avoid repetitive code in CSharp. 
 
## Getting Started
```c#
//Add reference to your project
//Add library reference in the class you want to use and define EasMe classes
using EasMe;
EaSQL _easql = new EasQL();
EasBox _easbox = new EasBox();
EasINI _easini = new EasINI();
EasLog _easlog = new EasLog();
EasDel _easdel = new EasDel();
EasAPI _easapi = new EasAPI();
EasMail _easmail = new EasMail();
EasReCaptcha _easrecaptcha = new EasReCaptcha();
```

# EasQL
 EasQL helps you with basic SQL commands. Execute queries, get tables, shrink and backup database features.

### GetTable Usage
```c#
SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
DataTable dt = _easql.GetTable("YOUR-CONNECTION-STRING",cmd);

SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Id = @id");
cmd.Parameters.AddWithValue("@id",1);
DataTable dt = _easql.GetTable("YOUR-CONNECTION-STRING",cmd);
```

### ExecNonQuery, ExecScalar, ExecStoredProcedure Usage
```c#
//ExecNonQuery executes query and returnes affected rows as int value
SqlCommand cmd = new SqlCommand("UPDATE Users SET Email = @email WHERE Id = @id");
cmd.Parameters.AddWithValue("@email","example@mail.com");
cmd.Parameters.AddWithValue("@id",1);
int affectedRows = _easql.ExecNonQuery("YOUR-CONNECTION-STRING",cmd);

//ExecScalar will return an object, convert it depending on what value you are expecting from query
SqlCommand cmd = new SqlCommand("SELECT Email FROM Users WHERE Id = @id");
cmd.Parameters.AddWithValue("@id",1);
object obj = _easql.ExecScalar("YOUR-CONNECTION-STRING",cmd);

//ExecStoredProcedure will return int value which will depends on the procedure
SqlCommand cmd = new SqlCommand("sendMail");
cmd.CommandType = CommandType.StoredProcedure;
cmd.Parameters.AddWithValue("@Username", "exampleusername");
int result = _sql.ExecStoredProcedure("YOUR-CONNECTION-STRING", cmd);
```

### BackupDatabase and ShrinkDatabase Usage
```c#
//BackupDatabase will create a backup of your database in the given path and will add date in file name
_easql.BackupDatabase("YOUR-CONNECTION-STRING","DATABASE-NAME","BACKUP-PATH");

//ShrinkDatabase will shrink your database and logs, this will not delete your data only will reduce the disk space of SQL logs
_easql.ShrinkDatabase("YOUR-CONNECTION-STRING","DATABASE-NAME");

//Database log default name is set DATABASENAME_log but you can change it in SQL management studio if its default you only need to "_log" string after database name
//If you don't give paramete dblogname it will set dblogname to default
_easql.ShrinkDatabase("YOUR-CONNECTION-STRING","DATABASE-NAME","DATABASE-LOG-NAME");
```

### TruncateTable, DropTable, DropDatabase Usage
```c#
//TruncateTable will clear all rows in a table but table it self will remain
_easql.TruncateTable("YOUR-CONNECTION-STRING","Users");

//DropTable will directly delete the table from database
_easql.DropTable("YOUR-CONNECTION-STRING","Users");

//DropDatabase will directly delete the database
//This action can not be undone be careful when using this
_easql.DropDatabase("YOUR-CONNECTION-STRING", "DATABASE-NAME");
```

# EasBox
 EasBox helps you with message boxes. Show easy and simple Message Boxes with your WinForm project.

# EasINI
 EasINI helps you with reading and writing .ini files.

# EasLog
 EasLog helps you with creating logs with your application with interval options.
 
# EasDel
 EasDel helps you with deleting files, directories and sub directories with logging feature.

# EasAPI
 EasAPI helps you with send get and post requests to APIs.

# EasMail
 EasMail helps you with SMTP, it makes sending mails a lot faster.

# EasReCaptcha
 EasReCaptcha helps you with validating Google ReCaptcha with your web app.

