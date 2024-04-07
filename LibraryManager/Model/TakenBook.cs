using System;

namespace LibraryManager.Model;

public class TakenBook
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Book Book { get; set; }
    public Customer Customer { get; set; }
    public DateTime TimeTaken { get; set; }
    public DateTime TimeToReturn { get; set; }
}