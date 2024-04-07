using System.Collections.Generic;
using System.Linq;
using LibraryManager.Model;

namespace LibraryManager.Data;

public class BookRepository : Repository<Book>
{
    public BookRepository(List<Book> initialData) : base(initialData)
    {
    }
}