using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.exception
{

    [Serializable]
    public class EmployeeLackInformationException : Exception
    {
        public EmployeeLackInformationException() { }
        public EmployeeLackInformationException(string message) : base(message) { }
        public EmployeeLackInformationException(string message, Exception inner) : base(message, inner) { }
        protected EmployeeLackInformationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
