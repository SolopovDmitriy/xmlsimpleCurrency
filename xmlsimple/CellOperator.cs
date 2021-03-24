using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlsimple
{
    class CellOperator
    {
        private List<string> _phoneCodes;
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public List<string> PhoneCodes
        {
            get
            {
                return _phoneCodes;
            }
        }

        public CellOperator(string name, List<string> phoneCodes)
        {
            _name = name;
            _phoneCodes = phoneCodes;
        }
        public override string ToString()
        {
            string data = $"Operator: {Name}; \nCodes: ";
            foreach (string item in _phoneCodes)
            {
                data += $"\n\t {item}, ";
            }
            return data;
        }
    }
}
