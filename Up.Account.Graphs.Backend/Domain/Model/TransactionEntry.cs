using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class TransactionEntry
    {
        public string Type { get; set; }
        public TransactionAttributes Attributes { get; set; }
        public TransactionRelationships Relationships { get; set; }
    }
}