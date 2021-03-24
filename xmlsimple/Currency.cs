using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlsimple
{
    class Currency
    {
        public int Id { get; }

        public string Name { get; }

        public double Rate { get; }

        public string ShortName { get; }

        // public DateTime ExchangeDate { get; }

        public Currency(int id, string name, double rate, string shortName)
        {
            Id = id;
            Name = name;
            Rate = rate;
            ShortName = shortName;
            // ExchangeDate = exchangeDate;
        }

        public override string ToString()
        {
            return $"id: {Id}; name: {Name}; rate: {Rate}; short name: {ShortName} ";
        }
    }
}
