using System;
using System.Collections.Generic;

namespace LibraryManager.Model;

public class Customer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}