using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.exception
{

    [Serializable]
    public class InvalidAgeException : Exception
    {
        public InvalidAgeException() { }
        public InvalidAgeException(string message) : base(message) { }
        public InvalidAgeException(string message, Exception inner) : base(message, inner) { }
        protected InvalidAgeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
