using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                DateTime nullDate = new DateTime();
                if (compensation.EffectiveDate.Equals(nullDate) )
                { compensation.EffectiveDate = DateTime.Now; }
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }
        public List<Compensation> GetAllByEmployee(string employeeId)
        {
            var compensations = _compensationRepository.GetAllByEmployee(employeeId);
            return compensations;
        }
    }
}
