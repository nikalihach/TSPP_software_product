using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPP
{
    abstract public class general_information
    {
        public string name;
        public string address;

        public string removing_extra_spaces(string str)
        {
            var newstr = string.Join(" ", str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()));
            return newstr;
        }
        public string fix_the_registry(string str)
        {
            string[] newstr = str.Split();
            for (int i = 0; i < newstr.Length; i++)
            {
                newstr[i] = char.ToUpper(newstr[i][0]) + newstr[i].Substring(1).ToLower();
            }
            str = string.Join(" ", newstr);
            return str;
        }
        public string return_address()
        {
            return address;
        }
    }
}
