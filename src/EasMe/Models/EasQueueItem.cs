namespace EasMe.Models;

internal record EasQueueItem(Action Action, int RetryCount = 0);
