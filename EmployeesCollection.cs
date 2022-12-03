using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    internal class EmployeesCollection
    {
        private List<Employee> employees = new List<Employee>();

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public void DeleteEmployeeAtIndex(int index)
        {
            employees.RemoveAt(index);
        }

        public void updateEmployeeInfo(int index, Employee modified)
        {
            employees[index] = modified;
        }

        public Employee getEmployeeAtIndex(int index)
        {
            return employees[index];
        }

        public Employee[] GetAllEmployees()
        {
            return employees.ToArray();
        }

        public bool isEmpty()
        {
            return employees.ToArray().Length == 0;
        }
    }
}
