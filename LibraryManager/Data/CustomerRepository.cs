using System.Collections.Generic;
using LibraryManager.Model;

namespace LibraryManager.Data;

public class CustomerRepository : Repository<Customer>
{
    public CustomerRepository(List<Customer> initialData) : base(initialData)
    {
    }
}