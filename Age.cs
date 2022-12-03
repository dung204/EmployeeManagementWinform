using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.exception;

namespace EmployeeManagement
{
    internal class Age
    {
        private readonly int _age;

        public Age(int age)
        {
            if(age < 0) { throw new InvalidAgeException(); }
            _age = age;
        }

        public Age(string age)
        {
            try
            {
                if (int.Parse(age.Trim()) < 0) { throw new InvalidAgeException(); }
                _age = int.Parse(age.Trim());
            } catch (FormatException) {
                throw new InvalidAgeException();
            }
        }

        public static implicit operator Age(int age)
        {
            return new Age(age);
        }

        public static implicit operator Age(string age)
        {
            return new Age(age);
        }

        public override string ToString()
        {
            return _age.ToString(); 
        }
    }
}
