# EasZip

```c#
//Archives given file paths to destination zip file path
public static void MakeZip(string[] files, string destination);

//Extracts Zip tfrom given file path to extract folder path
//If extract path does not exist it will create directory
public static void UnZip(string sourceZip, string extractFolder);
```