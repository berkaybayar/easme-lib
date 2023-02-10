
using System.Diagnostics;

namespace EasMe
{
 
    public class EasTask
    {

        public EasTask(byte parallelism = 1,int checkInterval = 250)
        {
            _parallelism = parallelism;
            _checkInterval = checkInterval;
        }  
        public List<Task> InQueue { get; private set; } = new();
        public List<Task> Running { get; private set; } = new();
        public long CompletedTaskCount { get; private set; }
        private bool _isCalledOnStart = false;
        private readonly byte _parallelism = 1;
        private readonly int _checkInterval = 250;
        public void AddToQueue(Task task)
        {
            lock (InQueue)
            {
                InQueue.Add(task);
            }
        }
        public void CallOnExit(){
            //TODO: Run all tasks
        }
        public void CallOnStart()
        {
            if (_isCalledOnStart) throw new Exception("AlreadyCalled");
            _isCalledOnStart = true;
            if (_parallelism == 1) _CallOnStart_Single();
            else _CallOnStart_Multi();
        }
        private void _CallOnStart_Single()
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
                        //Trace.WriteLine(CompletedTaskCount + " Task Complete");
                    }
                    else
                        Thread.Sleep((int)_checkInterval);
                }
            });
        }
        private void _CallOnStart_Multi()
        {

            Task.Run(() =>
            {
            
                while (true)
                {
                    if (InQueue.Count > _parallelism)
                    {
                        for (int i = 0; i < _parallelism; i++)
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
                        //Trace.WriteLine(CompletedTaskCount + " Task Complete");
                    }
                    else
                        Thread.Sleep((int)_checkInterval);
                }
            });
        }
      
    }
}

