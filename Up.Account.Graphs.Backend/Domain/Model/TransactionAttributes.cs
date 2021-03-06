using System;

namespace Domain.Model
{
    public class TransactionAttributes
    {
        public string Description { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedAt { get; set; }
        public TransactionAmount Amount { get; set; }
    }
}