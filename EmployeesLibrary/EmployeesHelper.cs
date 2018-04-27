using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

namespace EmployeesLibrary
{
    public class EmployeesHelper
    {
        private List<EmployeeModel> employees;
        private string employeesString;
        string docsPathToJson;

        public int MyProperty { get; set; }

        public EmployeesHelper(string pathToJson)
        {
            docsPathToJson = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), pathToJson);

            // On first run copy file to Application's Documents directory.
            if (!File.Exists(docsPathToJson))
            {
                File.Copy(pathToJson, docsPathToJson);
            }

            employeesString = File.ReadAllText(docsPathToJson);
            employees = new List<EmployeeModel>(JsonConvert.DeserializeObject<EmployeeModel[]>(employeesString));
        }

        public List<EmployeeModel> GetEmployees()
        {
            employees.Sort((emp1, emp2) => emp1.Id.CompareTo(emp2.Id));
            return employees;
        }

        public List<string> GetEmployeesAsStrings()
        {
            employees.Sort((emp1, emp2) => emp1.Id.CompareTo(emp2.Id));
            return employees.Select((e) => e.ToString()).ToList();
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            this.MyProperty = 8;
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public EmployeeModel GetEmployeeByMail(string mail)
        {
            return employees.FirstOrDefault(e => e.Mail == mail);
        }

        public EmployeeModel GetEmployeeByName(string name)
        {
            if (name == null)
            {
                return null;
            }

            var names = name.Split(' ');
            if(names.Length == 1)
            {
                return employees.FirstOrDefault(e => e.FirstName == names[0] || e.LastName == names[0]);
            }
            return employees.FirstOrDefault(e => e.FirstName == names[0] && e.LastName == names[1]);
        }

        public void AddEmployee(EmployeeModel employeeModel)
        {
            employees.Add(employeeModel);
            SaveEmployees();
        }

        public void AddEmployee(string empl)
        {
            if (empl == null)
            {
                return;
            }
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(empl);
            this.AddEmployee(new EmployeeModel()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Mail = employee.Mail,
            });
        }

        public string GetEmployee(string by)
        {
            var parsed = int.TryParse(by, out int id);
            EmployeeModel result = null;
            if (parsed)
            {
                result = GetEmployeeById(id);
            }
            else if (by.Contains("@"))
            {
                result = GetEmployeeByMail(by);
            }
            else
            {
                result = GetEmployeeByName(by);
            }

            return result != null ? result.ToString() : "Not found!";
        }

        public void DeleteEmployee(int selectedEmployeeId)
        {
            employees.Remove(employees.Find((empl) => empl.Id == selectedEmployeeId));
            SaveEmployees();
        }

        private void SaveEmployees()
        {
            File.WriteAllText(docsPathToJson, JsonConvert.SerializeObject(employees));
        }
    }
}