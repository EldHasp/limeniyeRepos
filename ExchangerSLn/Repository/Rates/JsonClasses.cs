using System.Collections.Generic;

namespace Repository.Rates
{
    public class RatesJson
    {
        public Motd motd { get; set; }
        public bool success { get; set; }
        public string _base { get; set; }
        public string date { get; set; }

        public Dictionary<string, decimal> rates  { get; set; }
    }

    public class Motd
    {
        public string msg { get; set; }
        public string url { get; set; }
    }
}
