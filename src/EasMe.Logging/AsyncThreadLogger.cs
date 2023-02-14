

namespace EasMe.Logging;

public abstract class AsyncThreadLogger<T> : IDisposable
{
    private readonly Queue<Action> _queue = new Queue<Action>();
    private readonly ManualResetEvent _hasNewItems = new(false);
    private readonly ManualResetEvent _terminate = new(false);
    private readonly ManualResetEvent _waiting = new(false);

    private readonly Thread _loggingThread;

    protected AsyncThreadLogger()
    {
        _loggingThread = new Thread(ProcessQueue)
        {
            IsBackground = true
        };
        // this is performed from a bg thread, to ensure the queue is serviced from a single thread
        _loggingThread.Start();
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

            Queue<Action> queueCopy;
            lock (_queue)
            {
                queueCopy = new Queue<Action>(_queue);
                _queue.Clear();
            }

            foreach (var log in queueCopy)
            {
                log();
            }
        }
    }

    public void LogMessage(T log)
    {
        lock (_queue)
        {
            _queue.Enqueue(() => AsyncLogMessage(log));
        }
        _hasNewItems.Set();
    }

    protected abstract void AsyncLogMessage(T log);


    public void Flush()
    {
        _waiting.WaitOne();
    }


    public void Dispose()
    {
        _terminate.Set();
        _loggingThread.Join();
        GC.SuppressFinalize(this);
    }
}
