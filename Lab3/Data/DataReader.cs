using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Lab3.Data.Models;
using Microsoft.Extensions.Logging;

namespace Lab3.Data
{
    public class DataReader
    {
        private const string PremiaTxtFilePath = "Data/premia.txt";
        private const string ZavodTxtFilePath = "Data/zavod.txt";

        private readonly ILogger _logger;

       
        public DataReader(ILogger<DataReader> logger)
        {
            _logger = logger;
        }

        public List<Employee> GetEmployees()
        {
            var lines = ReadTxtFile(ZavodTxtFilePath);

            var employes = new List<Employee>();
            foreach (var line in lines)
            {
                try
                {
                    var words = Regex.Split(line.Trim(), @"\s+");
                    var employee = new Employee
                    {
                        Code = int.Parse(words[0]),
                        LastName = words[1],
                        Gender = words[2],
                        WorkshopNumber = int.Parse(words[3]),
                        Position = words[4],
                        Salary = decimal.Parse(words[5])
                    };

                    employes.Add(employee);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }

            return employes;
        }

        public List<Premium> GetPremiums()
        {
            var lines = ReadTxtFile(PremiaTxtFilePath);

            var premiums = new List<Premium>();
            foreach (var line in lines)
            {
                try
                {
                    var words = Regex.Split(line.Trim(), @"\s+");
                    var employee = new Premium
                    {
                        EmployeeCode = int.Parse(words[0]),
                        Value = decimal.Parse(words[1]),
                        DateTime = DateTime.Parse(words[2])
                    };

                    premiums.Add(employee);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }

            return premiums;
        }

        private string[] ReadTxtFile(string path)
        {
            var lines = File.ReadLines(path);
            return lines.ToArray();
        }
    }
}