using System;

namespace LibraryManager.Model;

public class Log
{
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
}