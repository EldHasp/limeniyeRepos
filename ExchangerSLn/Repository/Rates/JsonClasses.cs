using Newtonsoft.Json;
using System.Collections.Generic;

namespace Repository.Rates
{
    public class RatesJson
    {
        [JsonProperty("count")]
        public string Count { get; set; }
        [JsonProperty("results")]
        public Dictionary<string, CurrencyResponse> rates { get; set; }
    }


    public class CurrencyResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("val")]
        public decimal Val { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("fr")]
        public string Fr { get; set; }
    }
}
//{"USD_UAH":{"id":"USD_UAH","val":27.948458,"to":"UAH","fr":"USD"}