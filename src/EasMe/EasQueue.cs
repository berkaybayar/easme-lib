using EasMe.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace EasMe;

/// <summary>
///   Runs a queue of Tasks in the background with a single thread
/// </summary>
public class EasQueue : IDisposable
{
  private readonly ManualResetEvent _hasNewItems = new(false);
  private readonly Queue<EasQueueItem> _queue = new();
  private readonly ManualResetEvent _terminate = new(false);

  private readonly Thread _thread;
  private readonly ManualResetEvent _waiting = new(false);

  public EasQueue() {
    _thread = new Thread(ProcessQueue) {
      IsBackground = true
    };
    // this is performed from a bg thread, to ensure the queue is serviced from a single thread
    _thread.Start();
  }


  public void Dispose() {
    _terminate.Set();
    _thread.Join();
    GC.SuppressFinalize(this);
  }


  private void ProcessQueue() {
    while (true) {
      _waiting.Set();
      var i = WaitHandle.WaitAny(new WaitHandle[] { _hasNewItems, _terminate });

      if (i == 1) return; // terminate was signaled 
      _hasNewItems.Reset();
      _waiting.Reset();
      if (_queue.Count == 0) continue;
      Queue<EasQueueItem> queueCopy;
      lock (_queue) {
        queueCopy = new Queue<EasQueueItem>(_queue);
        _queue.Clear();
      }


      foreach (var item in queueCopy) {
        var retry = 0;
        while (true) {
          try {
            retry++;
            item.Action();
            break;
          }
          catch (Exception) {
            if (retry > item.RetryCount)
              throw;
            Thread.Sleep(1000);
          }
        }
      }
    }
  }

  public void AddToQueue(Action action,
                         int retryCount = 0) {
    lock (_queue) {
      _queue.Enqueue(new EasQueueItem(action, retryCount));
    }

    _hasNewItems.Set();
  }


  public void Flush() {
    _waiting.WaitOne();
  }
}