// See https://aka.ms/new-console-template for more information

using System.CodeDom;
using System.Diagnostics;
using EasMe;
using EasMe.Extensions;
using EasMe.Logging;
using EasMe.Models;
using EasMe.Test;

//var dic = new Dictionary<int,int>
//{
//    { 1, 100 },
//    { 10, 1100 },
//    { 25, 2750 },
//    { 50, 6000 },
//    { 75, 9000 },
//    { 100, 13000 },
//    { 250, 35000 },
//    { 500, 75000 },
//    { 1000, 180000 }
//};

//var amount = GetCashAmount(dic, 275);
//Console.WriteLine(amount);
//int GetCashAmount(Dictionary<int, int> dictionary,int paymentAmount)
//{
//    var ordered = dictionary.OrderByDescending(x => x.Key);
//    var leftAmount = paymentAmount;
//    var cashAmount = 0;
//    for (var i = paymentAmount; i > 0; i--)
//    {
//        foreach (var item in ordered)
//        {
//            var num = leftAmount - item.Key;
//            if (num <= -1) continue;
//            leftAmount -= item.Key;
//            cashAmount += item.Value;
//            Console.WriteLine(item.Key);
//            break;
//        }
//    }
//    return cashAmount;
//}



//var list = new List<string>();
//var length = 6;


//Parallel.For(0L,100_000_000L, x =>
//{
//    var guid = EasGenerate.GetUniqueKey(5);
//    if (list.Contains(guid))
//    {
//        Console.WriteLine("AlreadyExists: " + guid + " x: " + x);
//        return;
//    }

//    lock (list)
//    {
//        list.Add(guid);
//    }
//    Trace.WriteLine(x);
//});

EasLogFactory.Configure(x =>
{
    x.ConsoleAppender = true;
});

Class1.Test();

Console.Read();