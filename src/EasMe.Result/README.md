# Result

A readonly struct to implement Result

You can also check [Ardalis.Result](https://github.com/ardalis/Result)

## Definition

```c#
//ReturnValue indicating where exactly method is ended
//It is used to replace Exception StackTrace 
//Every Fail Result a method returns each one should have different Rv
//This is needed in case where returning same Result Error for different if statements. 
//So Dev Team can keep track of where exactly problem come from inside Logs 
//This value should NOT be given to client, it may cause security leaks
public ushort Rv { get; init; } = ushort.MaxValue;

//Result Success Status
public bool IsSuccess => Rv == 0;

//Result Fail Status
public bool IsFailure => !IsSuccess;

//Result Error Code
//It is recommended to use Enum objects
public string ErrorCode { get; init; } = "None";

//An extra Error Array in case where you need to return multiple errors at once
//Example: Validation errors
public string[] Errors { get; init; }

//Indicates the importance of the Error Result
//If it is Success this is set to INFO
public ResultSeverity Severity { get; init; }
```

## Static Methods to create Result

```c#
public static Result Success();
public static Result Success(string operationName);
public static Result Warn(ushort rv, object errorCode, params string[] errors);
public static Result Error(ushort rv, object errorCode, params string[] errors);
public static Result Fatal(ushort rv, object errorCode, params string[] errors);
```

## Usage

```c#
var exampleSuccessResult = Result.Success("Auth.Login");
var exampleWarnResult = Result.Warn(1,"Auth.Login","User is deleted");
var exampleErrorResult = Result.Error(2,"ValidationError","Username is too short");
var exampleFatalResult = Result.Fatal(100,"DbInternalError");
```

# ResultData<{T}>

A readonly struct to implement IResultData<{T}>

## Definition

Inherits from IResult and addition to that implements nullable Data property

```c#
T? Data { get; init; }
```

## Static Methods to create ResultData<{T}>

```c#
public static ResultData<T> Success(T data);
public static ResultData<T> Success(T data,string operationName);
public static ResultData<T> Warn(ushort rv, object errorCode, params string[] errors);
public static ResultData<T> Error(ushort rv, object errorCode, params string[] errors);
public static ResultData<T> Fatal(ushort rv, object errorCode, params string[] errors);
```

## Other Methods in ResultData<{T}>

```c#
//Rv value sometimes can be a security leak so when returning Result from API its better to send it without Rv (Return Value)
public ResultData<T> WithoutRv();

//Converts ResultData<T> to Result if its success T Data will not be in it
public Result ToResult();

//In case where using methods returning Result inside of a method returns Result
//It may be required to multiply the ReturnValue (Rv) from the inner method
public Result ToResult(byte multiplyRv);
```

## Implicit Operators

```c#
public static implicit operator ResultData<T>(T? value);

//You can only convert from Result to IResultData<{T}> when Result status is failed
//In case when Result is success it will thro InvalidOperationException
public static implicit operator ResultData<T>(Result result); 
```

## Usage

```c#
var exampleSuccessResult = Result.Success("Auth.Login");
var exampleWarnResult = Result.Warn(1,"Auth.Login","User is deleted");
var exampleErrorResult = Result.Error(2,"ValidationError","Username is too short");
var exampleFatalResult = Result.Fatal(100,"DbInternalError");
```

## Real Usage Examples

```c#
public ResultData<User> GetUser(int id)
{
  var user = new User(); 
  //Fill user with from Database
  if(user is null)
  {
   return Result.Warn(1,"User is not exist"); //Result is converted to ResultData with implicit operator
  }
  if(!user.IsValid)
  {
   return Result.Warn(2,"User is not valid"); //Result is converted to ResultData with implicit operator
  }
  if(user.DeletedDate.HasValue)
  {
   return Result.Warn(3,"User is deleted"); //Result is converted to ResultData with implicit operator
  }
  return user; //User is converted to ResultData<User> and if user is not null ResultData status will be success
}

public Result UpdateUser(User user)
{
  var userExists = _db.CheckIfUserExists(user.Id);
  if(!userExists)
  { 
   return Result.Warn(1, "User not exists);
  }
  return true; //Will return base Result Success
  //Or
  return Result.Success("UpdateUser");
}
```