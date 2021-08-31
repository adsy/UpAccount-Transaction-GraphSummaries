using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class TransactionData
    {
        public TransactionData()
        {
            Categories = new Dictionary<string, double>();
            OutgoingTransactions = new Dictionary<string, double>();
            IncomingTransactions = new Dictionary<string, double>();
        }

        public List<TransactionEntry> Data { get; set; }
        public Dictionary<string, double> Categories { get; set; }
        public double TotalOutflow { get; set; }
        public double TotalInflow { get; set; }
        public Dictionary<string,double> OutgoingTransactions { get; set; }
        public Dictionary<string,double> IncomingTransactions{ get; set; }
    }
}