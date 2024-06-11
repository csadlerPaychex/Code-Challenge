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
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;
        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }

        //I went a little overboard with methods here.  Most of these are currently unused, but they could form the groundwork for adding new features to the API. 
        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            var adding = _compensationContext.Add(compensation);
            return compensation;
        }
        public Compensation GetById(string compensationId)
        {
            var currentCompensation = _compensationContext.Compensations.SingleOrDefault(e => e.CompensationId == compensationId);
            return currentCompensation;
        }
        public List<Compensation> GetAllByEmployee(string employeeId)
        {
            var compensationList = _compensationContext.Compensations.Where(e => e.EmployeeId == employeeId).ToList();
            return compensationList;
        }
        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.Remove(compensation).Entity;
        }
        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }

    }
}
