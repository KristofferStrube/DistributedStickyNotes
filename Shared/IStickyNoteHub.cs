namespace Shared;

public interface IStickyNoteHub
{
    Task<List<Note>> LoadNotes();
    Task CreateNote(double x, double y);
    Task UpdateNoteText(Guid id, string text);
    Task<bool> LockNote(Guid id);
    Task MoveNote(Guid id, double x, double y);
    Task DeleteNote(Guid id);
    Task ClearNotes();
}
