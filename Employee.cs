using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    internal class Employee
    {
        public Employee(string name, Age age, string address, string room, string position)
        {
            Name = name.Trim();
            Age = age;
            Address = address.Trim();
            Room = room.Trim();
            Position = position.Trim();
        }

        public string Name { get; set; }
        public Age Age { get; set; }
        public string Address { get; set; }
        public string Room { get; set;  }
        public string Position { get; set; }
    }
}
