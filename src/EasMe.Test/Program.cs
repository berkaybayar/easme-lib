

using Ardalis.Result;
using EasMe.Extensions;

Console.WriteLine("");
var test = TEst();
Console.WriteLine(test.ToJsonString());
Console.Read();


 Result TEst()
{
    if (true == true)
    {
        return Result.SuccessWithMessage("Error");
    }
    else
    {
        return Result.Unauthorized();
    }
}