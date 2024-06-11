using challenge.Models;
using Microsoft.AspNetCore.ApplicationInsights.HostingStartup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class EmployeeDataSeeder
    {
        private EmployeeContext _employeeContext;
        private const String EMPLOYEE_SEED_DATA_FILE = "resources/EmployeeSeedData.json";

        public EmployeeDataSeeder(EmployeeContext employeeContext)
        {
            System.Diagnostics.Debug.WriteLine("Entering Seeder");
            _employeeContext = employeeContext;
        }

        public async Task Seed()
        {
            Console.WriteLine("Entering Seeder Task");
            if (!_employeeContext.Employees.Any())
            {
                List<Employee> employees = LoadEmployees();
                _employeeContext.Employees.AddRange(employees);

                await _employeeContext.SaveChangesAsync();
            }
        }

        private List<Employee> LoadEmployees()
        {
            System.Diagnostics.Debug.WriteLine("Entering Seeder Load Employees");
            using (FileStream fs = new FileStream(EMPLOYEE_SEED_DATA_FILE, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();

                List<Employee> employees = serializer.Deserialize<List<Employee>>(jr);
                FixUpReferences(employees);

                return employees;
            }
        }

        private void FixUpReferences(List<Employee> employees)
        {
            System.Diagnostics.Debug.WriteLine($"Entering Seeder Reference Fix {employees}");
            var employeeIdRefMap = from employee in employees
                                select new { Id = employee.EmployeeId, EmployeeRef = employee };

            System.Diagnostics.Debug.WriteLine($"Ref Map    {employeeIdRefMap.First()}");

            employees.ForEach(employee =>
            {
                

                if (employee.DirectReports != null)
                {
                    var referencedEmployees = new List<Employee>(employee.DirectReports.Count);
                    System.Diagnostics.Debug.WriteLine($"Entering Employee For each employee {employee.FirstName}");
                    System.Diagnostics.Debug.WriteLine($"Current Direct Reports {employee.DirectReports.First()}");
                    employee.DirectReports.ForEach(report =>
                    {
                        System.Diagnostics.Debug.WriteLine($"Current Report {report.EmployeeId}");
                        var referencedEmployee = employeeIdRefMap.First(e => e.Id == report.EmployeeId).EmployeeRef;
                        referencedEmployees.Add(referencedEmployee);
                        if (referencedEmployee != null)
                        { System.Diagnostics.Debug.WriteLine($"Referenced Employee output: {referencedEmployees.First().EmployeeId}"); }
                        
                    });
                    employee.DirectReports = referencedEmployees;
                    System.Diagnostics.Debug.WriteLine($"Final Result. Employee reports {employee.DirectReports.First().EmployeeId}");
                }
            });
        }
    }
}
