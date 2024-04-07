using System.Collections.Generic;
using LibraryManager.Model;

namespace LibraryManager.Data;

public class TakenBookRepository : Repository<TakenBook>
{
    public TakenBookRepository(List<TakenBook> initialData) : base(initialData)
    {
    }
}