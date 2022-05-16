# Introduction
 Collection of classes that will save you time and help you avoid repetitive code in CSharp. 
 
## Implementation
```
Add reference to your project
Add library reference in the class you want to use
using EasMe;
```

# EasQL
 EasQL helps you with basic SQL commands. Execute queries, get tables, shrink and backup database features.
 
## Implementation and Usage
```
Add reference to EasQL
EaSQL _easql = new EasQL();
```

### GetTable Usage
```
SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
DataTable dt = _easql.GetTable(*YOUR-CONNECTION-STRING*,cmd)

SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Id = @id");
cmd.Parameters.AddWithValue("@id",1);
DataTable dt = _easql.GetTable(*YOUR-CONNECTION-STRING*,cmd)
```

### ExecNonQuery, ExecScalar, ExecStoredProcedure Usage
```
SqlCommand cmd = new SqlCommand("UPDATE Users SET Email = @email WHERE Id = @id");
cmd.Parameters.AddWithValue("@email","example@mail.com");
cmd.Parameters.AddWithValue("@id",1);
int affectedRows = _easql.ExecNonQuery(*YOUR-CONNECTION-STRING*,cmd)

SqlCommand cmd = new SqlCommand("SELECT Email FROM Users WHERE Id = @id");
cmd.Parameters.AddWithValue("@id",1);
object obj = _easql.ExecScalar(*YOUR-CONNECTION-STRING*,cmd)

SqlCommand cmd = new SqlCommand("sendMail");
cmd.CommandType = CommandType.StoredProcedure;
cmd.Parameters.AddWithValue("@Username", "exampleusername");
int result = _sql.ExecStoredProcedure(*YOUR-CONNECTION-STRING*, cmd);
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

