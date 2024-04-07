using System;

namespace LibraryManager.Model;

public class Book
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public string Author { get; set; }
    public int Count { get; set; } = 3;

    public override string ToString()
    {
        return $"{Author} - {Title}";
    }
}