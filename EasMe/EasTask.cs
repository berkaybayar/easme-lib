
using System.Diagnostics;

namespace EasMe
{
    /// <summary>
    /// Simple task static task runner thread safe. It will run one task at a time.
    /// </summary>
    public static class EasTask
    {
       
        public static List<Task> InQueue { get; private set; } = new();
        public static List<Task> Running { get; private set; } = new();
        public static long CompletedTaskCount { get; set; }
        public static void AddToQueue(Task task)
        {
            lock (InQueue)
            {
                InQueue.Add(task);
            }
        }
        
        public static void CallOnStart()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (InQueue.Count > 0)
                    {
                        var task = InQueue.First();
                        InQueue.RemoveAt(0);
                        Running.Add(task);
                        task.Start();
                        task.Wait();
                        Running.Remove(task);
                        CompletedTaskCount++;
                        Trace.WriteLine(CompletedTaskCount + " Task Complete");

                    }
                }
            });
        }
        public static void CallOnStart(byte max)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (InQueue.Count > max)
                    {
                        for (int i = 0; i < max; i++)
                        {
                            var task = InQueue[i];
                            if (task is null) continue;
                            InQueue.Remove(task);
                            task.Start();
                            Running.Add(task);
                        }
                        Task.WaitAll(Running.ToArray());
                        CompletedTaskCount += Running.Count;
                        Running.Clear();
                        Trace.WriteLine(CompletedTaskCount + " Task Complete");
                    }
                }
            });
        }
        //public static void CallOnStart_NoLimit()
        //{
        //    Task.Run(() =>
        //    {
        //        while (true)
        //        {
        //            if (InQueue.Count > 0)
        //            {
        //                foreach(var task in InQueue)
        //                {
        //                    Running.Add(task);
        //                    task.Start();
        //                }
        //                Task.WhenAll(Running);
        //                CompletedTaskCount += Running.Count;
        //                Running.Clear();
        //                Trace.WriteLine(CompletedTaskCount + " Task Complete");
        //            }
        //        }
        //    });
        //}
    }
}

