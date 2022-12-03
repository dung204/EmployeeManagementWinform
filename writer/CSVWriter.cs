using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.writer
{
    internal class CSVWriter : Writer
    {
        public void WriteFile(string dest, string content)
        {
            File.WriteAllText(dest, content);
        }

        public void WriteFile(string dest, string[] propNames, string[] values)
        {
            string header = $"{string.Join(",", propNames)}\n";
            string content = "";
            foreach (string value in values)
            {
                content += $"{value}\n";
            }
            File.WriteAllText(dest, header + content);
        }
    }
}
