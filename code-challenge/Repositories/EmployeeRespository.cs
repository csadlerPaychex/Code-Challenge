using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            var adding = _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            var currentEmployee = _employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
            if (currentEmployee == null)
            {
                return null;
            }
            LoadDirectReports(currentEmployee);
            return currentEmployee;
        }

        private void LoadDirectReports (Employee employee)
        {
            _employeeContext.Entry(employee).Collection(e => e.DirectReports).Load();
            foreach (var directReport in employee.DirectReports)
            {
                LoadDirectReports(directReport);
            }
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
