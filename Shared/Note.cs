namespace Shared;

public class Note
{
    private CancellationTokenSource? cts;

    public Note() { }

    public Note(double x, double y)
    {
        Id = Guid.NewGuid();
        LastEdited = DateTimeOffset.UtcNow;
        X = x;
        Y = y;
    }

    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
    public string? LastLockingUser { get; set; }
    public DateTimeOffset LastEdited { get; set; }

    public bool CanLock(string? connectionId)
    {
        return DateTimeOffset.UtcNow.Subtract(LastEdited).TotalSeconds > 1
            || LastLockingUser is null
            || LastLockingUser == connectionId;
    }

    public void Lock(string? connectionId)
    {
        LastLockingUser = connectionId;
        LastEdited = DateTimeOffset.UtcNow;
    }

    public bool TryLock(string? connectionId, IStickyNoteClient? others = null)
    {
        lock (this)
        {
            if (!CanLock(connectionId)) return false;
            Lock(connectionId);

            cts?.Cancel();
            if (others is null) return true;

            cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(async parameter =>
            {
                CancellationToken token = (CancellationToken)parameter!;
                await Task.Delay(1000);
                if (token.IsCancellationRequested) return;
                await others.NoteUpdated(this);
            }), cts.Token);
            return true;
        }
    }
}
