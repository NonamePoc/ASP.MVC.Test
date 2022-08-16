using System.Collections.Generic;
using Lab3.Data.Models;

namespace Lab3.Data
{
    public class DataContext
    {
        public List<User> Users { get; set; }

        public List<Employee> Employees { get; set; }

        public List<Premium> Premiums { get; set; }

        public DataContext(DataReader dataReader)
        {
            InitializeData(dataReader);
        }

        private void InitializeData(DataReader dataReader)
        {
            Users = new List<User>
            {
                new User
                {
                    Login = "user",
                    Password = "password1"
                },
                new User
                {
                    Login = "admin",
                    Password = "admin_pass"
                }
            };

            Employees = dataReader.GetEmployees();
            Premiums = dataReader.GetPremiums();
        }
    }
}