using System;

namespace CardFile.Business.Models
{
    public class Company
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string TaxId { get; set; }     
        public string Industry { get; set; }   
        public string CEO { get; set; }        
        public int EmployeeCount { get; set; }
        public decimal AnnualRevenue { get; set; }

       
        public string AnnualRevenueText => AnnualRevenue.ToString("N0") + " ₽";
    }
} 