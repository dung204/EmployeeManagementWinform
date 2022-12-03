using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.writer
{
    internal class JSONWriter : Writer
    {
        public void WriteFile(string dest, string content)
        {
            File.WriteAllText(dest, content);
        }

        public void WriteFile(string dest, string[] propNames, Hashtable[] data)
        {
            string result = "[\n";
            for(int i = 0; i < data.Length; i++)
            {
                string content = "\t{\n";
                for(int j = 0; j < propNames.Length; j++)
                {
                    content += $"\t\t\"{propNames[j]}\": \"{data[i][propNames[j]]}\"{(j == propNames.Length - 1 ? "" : ",")}\n";
                }

                const string closingBracket = "}";
                content += $"\t{closingBracket}{(i == data.Length - 1 ? "" : ",")}\n";
                result += content;
            }
            result += "]";
            File.WriteAllText(dest, result);
        }
    }
}
