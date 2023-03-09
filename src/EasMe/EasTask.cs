

namespace EasMe;

/// <summary>
/// Runs a queue of Tasks in the background with a single thread 
/// </summary>
public class EasTask : IDisposable
{
    private readonly Queue<Action> _queue = new();
    private readonly ManualResetEvent _hasNewItems = new(false);
    private readonly ManualResetEvent _terminate = new(false);
    private readonly ManualResetEvent _waiting = new(false);

    private readonly Thread _thread;

    public EasTask()
    {
        _thread = new Thread(ProcessQueue)
        {
            IsBackground = true
        };
        // this is performed from a bg thread, to ensure the queue is serviced from a single thread
        _thread.Start();
    }


    private void ProcessQueue()
    {
        while (true)
        {
            _waiting.Set();
            var i = WaitHandle.WaitAny(new WaitHandle[] { _hasNewItems, _terminate });
            
            if (i == 1) return; // terminate was signaled 
            _hasNewItems.Reset();
            _waiting.Reset();
            if (_queue.Count == 0) continue;
			Queue<Action> queueCopy;
            lock (_queue)
            {
                queueCopy = new Queue<Action>(_queue);
                _queue.Clear();
            }
            foreach (var action in queueCopy)
            {
                action();
            }
        }
    }

    public void AddToQueue(Action action)
    {
        lock (_queue)
        {
            _queue.Enqueue(action);
        }
        _hasNewItems.Set();
    }
    

    public void Flush()
    {
        _waiting.WaitOne();
    }


    public void Dispose()
    {
        _terminate.Set();
        _thread.Join();
        GC.SuppressFinalize(this);
    }
}
