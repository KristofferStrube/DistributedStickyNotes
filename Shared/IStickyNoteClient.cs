namespace Shared;

public interface IStickyNoteClient
{
    Task NoteCreated(Note note);
    Task NoteUpdated(Note note);
    Task NoteDeleted(Guid id);
} 
