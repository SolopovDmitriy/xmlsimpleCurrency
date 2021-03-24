using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace xmlsimple
{
    class CurrencyConverter
    {
        private string _href = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange";        
        private string _fullHref;
        private DateTime _date;
        public Dictionary<string, Currency> Currencies { get; set; }

        public string Date
        {
            get
            {
                return _date.ToString("MM/dd/yyyy");
            }
        }


        //****** public string GetDate()
        //{           
        //     return _date.ToString("MM/dd/yyyy");           
        //}

        public void ChangeDateTime(DateTime dateTime)
        {            
            _fullHref = _href  + "?date=" + dateTime.ToString("yyyyMMdd");  // https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange   ?date=    20210123
            ReloadCurrencyData(_fullHref);
        }
        
        private void ReloadCurrencyData(string hrefPath)
        {
            //string currency = new WebClient().DownloadString(hrefPath);
            //Console.WriteLine(currency);
            //byte[] bytes = Encoding.Default.GetBytes(currency);
            //currency = Encoding.UTF8.GetString(bytes);
            //// Console.WriteLine(currency);
            //XmlDocument xDocument = new XmlDocument();
            //xDocument.LoadXml(currency);
            // ===================================================================

            //byte[] bytes = new WebClient().DownloadData(hrefPath);
            //string currencyDocument = Encoding.UTF8.GetString(bytes);
            // ===================================================================



            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            string currencyDocument = client.DownloadString(hrefPath);
            // Console.WriteLine(currency);
            Console.WriteLine(hrefPath);
            XmlDocument xDocument = new XmlDocument();
            xDocument.LoadXml(currencyDocument); //   строку пконвертируем  в  XmlDocument (где находятся узлы)
            XmlNode root = xDocument.DocumentElement;//получаем  корень xDocument (root ==exchange), у exchange есть дети
            // Console.WriteLine(root.Name);
            //Console.WriteLine(root.ChildNodes.Count);
            //for (int i = 0; i < root.ChildNodes.Count; i++)
            //{
            //    XmlNode currencyNode = root.ChildNodes[i];                
            //}
            
            _date = Convert.ToDateTime(root.FirstChild.ChildNodes[4].InnerText);//FirstChild = currency, root.FirstChild.ChildNodes[4] = <exchangedate>25.03.2021</exchangedate> ;
                                                                                //root.FirstChild.ChildNodes[4].InnerText = 25.03.2021
            Currencies = new Dictionary<string, Currency>();// создаем пустой словарь
            foreach (XmlNode currencyNode in root.ChildNodes)// currencyNode - currency;  root.ChildNodes - currencies
            {
                XmlElement currencyElement = (XmlElement)currencyNode;
                int id = Convert.ToInt32(currencyNode.ChildNodes[0].InnerText);
                string name = currencyNode.ChildNodes[1].InnerText;
                double rate = Double.Parse(currencyNode.ChildNodes[2].InnerText, CultureInfo.InvariantCulture);// CultureInfo.InvariantCulture использует точку  в  роли разделителя дробной части
                string shortName = currencyNode.ChildNodes[3].InnerText;
                //DateTime exchangeDate = Convert.ToDateTime(currencyNode.ChildNodes[4].InnerText);
                Currency currency = new Currency(id, name, rate, shortName);
                Currencies.Add(shortName, currency);// добавляем обЪект currency с ключом shortName (например - USD)
                //Console.WriteLine(currencyNode.ChildNodes[1].Name + " " + currencyNode.ChildNodes[1].InnerText); 
                // Console.WriteLine(currencyElement["txt"].Name + " " + currencyElement["txt"].InnerText);
            }

        }

        public  CurrencyConverter()
        {
            _fullHref = _href;  // _href =  "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange"; 
            ReloadCurrencyData(_fullHref);            
        }
    }
}
