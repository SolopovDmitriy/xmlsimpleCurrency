using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlsimple
{
    static class PhoneCodes
    {
        public static string Path = @"..\..\PhoneCodes.xml";
        public static class Root
        {
            public static string ElementName = "Operators";
            public static class Operator
            {
                public static string ElementName = "Operator";
                public static class Attributes
                {
                    public static string Name = "Name";
                }
                public static class PhoneCode
                {
                    public static string ElementName = "PhoneCode";
                    public static class Attributes
                    {
                        public static string Value = "Value";
                    }
                }
            }
        }
    }
}
