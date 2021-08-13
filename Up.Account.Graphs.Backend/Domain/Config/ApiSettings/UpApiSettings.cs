using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Config.ApiSettings
{
    public class UpApiSettings
    {
        public const string UpApiSettingsKey = "ApiSettings:Up-Api";
        public const string TransactionListKey = "TransactionsByDate";

        public string ClientName { get; set; }
        public string BaseClient { get; set; }
        public Dictionary<string, string> Endpoints { get; set; }
        public Dictionary<string, string> ApiKey { get; set; }

        public string GetTransactionsListUri => Endpoints[TransactionListKey];
    }
}