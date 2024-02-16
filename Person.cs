using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pirveli_etapis_proeqti
{
    internal class Person
    {
        [JsonProperty(Order = 1)]
        public string name {  get; set; }

        [JsonProperty(Order = 2)]
        public string lastname {  get; set; }

        [JsonProperty(Order = 3)]
        public CardDetails Carddetails { get; set; }

        [JsonProperty(Order = 4)]
        public string pin { get; set; }

        [JsonProperty(Order = 5)]
        public List<Transaction> transactions;
        
    }
}
