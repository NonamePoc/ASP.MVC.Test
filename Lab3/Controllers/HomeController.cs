using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Lab3.Data;
using Lab3.Data.Models;
using Lab3.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lab3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var viewModels = new HomePageViewModel
            {
                Employees = _dataContext.Employees,
                Premiums = _dataContext.Premiums
            };

            return View(viewModels);
        }

        public IActionResult Task1()
        {
            var task1Result = GetTask1Result();
            return View(task1Result);
        }

        public IActionResult Task2()
        {
            var task2Result = GetTask2Result();
            return View(task2Result);
        }

        private Dictionary<string, decimal> GetTask2Result()
        {
            var positionsWithTotalIncome = new Dictionary<string, decimal>();
            foreach (var employee in _dataContext.Employees)
            {
                var employeeIncome = employee.Salary;

                var premium = GetEmployeePremium(employee.Code);
                employeeIncome += premium?.Value ?? 0;

                if (positionsWithTotalIncome.ContainsKey(employee.Position))
                {
                    positionsWithTotalIncome[employee.Position] += employeeIncome;
                }
                else
                {
                    positionsWithTotalIncome.Add(employee.Position, employeeIncome);
                }
            }

            return positionsWithTotalIncome;
        }

        private Dictionary<int, List<Employee>> GetTask1Result()
        {
            var employeesWorkshopGroups = new Dictionary<int, List<Employee>>();
            foreach (var employee in _dataContext.Employees)
            {
                if (employee.Gender.Equals("Чоловік", StringComparison.InvariantCultureIgnoreCase))
                {
                    var premium = GetEmployeePremium(employee.Code);

                    if (premium?.DateTime.Year != DateTime.Now.Year)
                    {
                        if (employeesWorkshopGroups.ContainsKey(employee.WorkshopNumber))
                        {
                            employeesWorkshopGroups[employee.WorkshopNumber].Add(employee);
                        }
                        else
                        {
                            var newWorkshopGroupEmployees = new List<Employee>
                            {
                                employee
                            };

                            employeesWorkshopGroups.Add(employee.WorkshopNumber, newWorkshopGroupEmployees);
                        }
                    }
                }
            }

            return employeesWorkshopGroups;
        }

        private Premium GetEmployeePremium(int employeeCode)
        {
            foreach (var premium in _dataContext.Premiums)
            {
                if (premium.EmployeeCode == employeeCode)
                {
                    return premium;
                }
            }

            return null;
        }
    }
}