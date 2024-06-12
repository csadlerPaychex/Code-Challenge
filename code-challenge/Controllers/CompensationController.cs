using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using challenge.Controllers;

namespace challenge.Controllers
{
    [Route("api/employee/{employeeId}/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        private readonly new IEmployeeService _employeeService;

        public IEmployeeService EmployeeService => _employeeService;

        public CompensationController(ILogger<EmployeeController> logger, ICompensationService compensationService, IEmployeeService employeeService)
        {
            _logger = logger;
            _compensationService = compensationService;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Compensation compensation, [FromRoute] string employeeId)
        {
            if (checkEmployee(employeeId) == false)
                return NotFound();
            compensation.EmployeeId = employeeId;
            var result = _compensationService.Create(compensation);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetAllByEmployee([FromRoute] string employeeId)
        {
            if (checkEmployee(employeeId) == false)
                return NotFound();
            var result = _compensationService.GetAllByEmployee(employeeId);
            return Ok(result);
        }
        public bool checkEmployee(string employeeId)
        {
           // Create a method for identfying bad employee IDs.
            var existingEmployee = EmployeeService.GetById(employeeId);
            if (existingEmployee == null)
                return false;
            return true;
        }
    }
}
