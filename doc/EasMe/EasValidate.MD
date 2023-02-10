# EasValidate

```c#
public static bool IsValidEmail(this string str);
public static bool IsValidIPAddress(this string value, out IPAddress? IpAddress);
public static bool IsValidIPAddress(this string IpAddress);
public static bool IsValidFilePath(this string path);
public static bool IsValidMACAddress(this string macAddress);
public static bool IsValidPort(this int port);
public static bool HasSpecialChars(this string yourString, string allowedChars = "");
public static bool IsStrongPassword(this string password, 
                                    string allowedChars, 
                                    byte minLength = 6, 
                                    byte maxLength = 16, 
                                    byte minUpperCaseCount = 1, 
                                    byte minLowerCaseCount = 1, 
                                    byte minNumberCount = 1, 
                                    byte minSpecialCharCount = 1);
public static bool IsUrlImage(this string URL);
public static bool IsUrlVideo(this string URL);
public static bool IsValidURL(this string url);
public static bool IsValidConnectionString(this string yourConn);
public static bool IsCreditCardInfoValid(string cardNo, string expiryDate, string cvv);
```