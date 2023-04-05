using Shared;

namespace Server;

public static class StaticStorage
{
    public static List<Note> Notes { get; set; } = new();
}
