using System.Collections.Generic;
using Lab3.Data.Models;

namespace Lab3.Models
{
    public class HomePageViewModel
    {
        public List<Employee> Employees { get; set; }
        
        public List<Premium> Premiums { get; set; }
    }
}