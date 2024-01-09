namespace EasMe;

internal record EasQueueItem(Action Action, int RetryCount = 0);