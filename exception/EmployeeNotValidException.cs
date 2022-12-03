using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.exception
{

    [Serializable]
    internal class EmployeeNotValidException : Exception
    {
        public EmployeeNotValidException() { }
        public EmployeeNotValidException(string message) : base(message) { }
        public EmployeeNotValidException(string message, Exception inner) : base(message, inner) { }
        protected EmployeeNotValidException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
