# EasTask
Async background task runner with options.

It will check new Tasks in Queue with given interval.

When parallelism is higher than 1, it will wait that number of Tasks to be added to the Queue and then will run all at once and will wait all.

## Definition
```c#
//Create instance
public class EasTask{
    //Paralellism and Check Interval Miliseconds
    public EasTask(byte parallelism = 1,int checkInterval = 250);
}
```

## Usage

Create instance in a static class top-level
```c#
public static class ExampleClass{
    public static readonly EasTask easTask = new EasTask(10,250);
}
```

Program.cs
```c#
//This method can only be called once in application runtime
//Calling it twice or more will throw exception
ExampleClass.easTask.CallOnStart();
```

Another class or method
```c#
//This method can only be called once in application runtime
//Calling it twice or more will throw exception
var task = new Task(() => {
    Console.WriteLine("Hello");
});
ExampleClass.easTask.AddToQueue(task);
```

On Exit
```c#
//Run this to finish all tasks before exiting application
AppDomain.CurrentDomain.ProcessExit += 
    (sender, eventArgs) => ExampleClass.easTask.CallOnExit();
```