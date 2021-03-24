using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace xmlsimple
{
    class Program
    {
        private static List<string> _phoneCodes;

        private static List<CellOperator> _cellOperators;

        private static void LoadPhoneCodes() {
            if (File.Exists(PhoneCodes.Path))
            {
                _phoneCodes = new List<string>();
                _cellOperators = new List<CellOperator>();

                XDocument xDocument = XDocument.Load(PhoneCodes.Path);
                IEnumerable<XElement> operatorElements = 
                    xDocument.Element(PhoneCodes.Root.ElementName).Elements(PhoneCodes.Root.Operator.ElementName);

                Console.WriteLine("Операторы ---------------------------start");
                foreach (XElement cellOperator in operatorElements)
                {
                    IEnumerable<XElement> phoneCodes = cellOperator.Elements(PhoneCodes.Root.Operator.PhoneCode.ElementName);
                    List<string> tmp = new List<string>();
                    foreach (XElement oneCode in phoneCodes)
                    {
                        XAttribute xAttribValue = oneCode.Attribute(PhoneCodes.Root.Operator.PhoneCode.Attributes.Value);
                        //Console.WriteLine(xAttribValue.Value);
                        _phoneCodes.Add(xAttribValue.Value);
                        tmp.Add(xAttribValue.Value);
                    }
                    _cellOperators.Add(new CellOperator(cellOperator.Attribute(PhoneCodes.Root.Operator.Attributes.Name).Value, tmp));
                }
                Console.WriteLine("Операторы ---------------------------end");
            }
            else
            {
                throw new FileNotFoundException("File NOT FOUND");
            }          
        }

        static void Main(string[] args)
        {
            /*LoadPhoneCodes();
            foreach (var item in _cellOperators)
            {
                Console.WriteLine(item);
            }*/
            //получаем курс валюты по текущей дате 25.03.2021
            CurrencyConverter currencyConverter = new CurrencyConverter();
            Console.WriteLine(currencyConverter.Date);// ******  Console.WriteLine(currencyConverter.GetDate());
            Console.WriteLine(currencyConverter.Currencies["USD"]);
            Console.WriteLine();

            //получаем курс валюты по текущей дате 15.01.2021 ----https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date=20210123
            currencyConverter.ChangeDateTime(new DateTime(2021, 1, 15));
            Console.WriteLine(currencyConverter.Date);
            Console.WriteLine(currencyConverter.Currencies["USD"]);
            Console.WriteLine();

            currencyConverter.ChangeDateTime(new DateTime(2021, 1, 23));
            Console.WriteLine(currencyConverter.Date);
            Console.WriteLine(currencyConverter.Currencies["USD"]);


            //int x = 26;
            //Console.WriteLine("Hex: {0:X}", x);
            //Console.WriteLine(x);
            //int a = 0x00001A + 10;
            //Console.WriteLine(a + x);

            Console.ReadKey();
        }

    }
}
