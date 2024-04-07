using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Model;

namespace LibraryManager.Data;

public class LogRepository : Repository<Log>
{
    public LogRepository(List<Log> initialData) : base(initialData)
    {
    }
}