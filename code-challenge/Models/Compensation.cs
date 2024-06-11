using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge.Models
{
    public class Compensation
    {
        [Key] public string CompensationId { get; set; } 
        [ForeignKey("EmployeeId") ] public string EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
