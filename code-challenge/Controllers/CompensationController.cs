using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;


namespace challenge.Controllers
{
    [Route("api/employee/{employeeId}/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<EmployeeController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Compensation compensation, [FromRoute] string employeeId)
        {
            compensation.EmployeeId = employeeId;
            var result = _compensationService.Create(compensation);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetAllByEmployee([FromRoute] string employeeId)
        {
            var result = _compensationService.GetAllByEmployee(employeeId);
            return Ok(result);
        }
    }
}
