using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    //I went a little overboard with methods here. Most of these are currently unused, but they could form the groundwork for adding new features to the API. 
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
        Task SaveAsync();
    }
}