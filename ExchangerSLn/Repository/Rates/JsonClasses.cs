using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Rates
{
    class JsonClasses
    {
    }

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

    public class Rates
    {
        public float CHF { get; set; }
        public float CNY { get; set; }
        public float EUR { get; set; }
        public float KZT { get; set; }
        public float RUB { get; set; }
        public float VND { get; set; }
    }
    //{"motd":{"msg":"If you or your company use this project or like what we doing, please consider backing us so we can continue maintaining and evolving this project.","url":"https://exchangerate.host/#/donate"},"success":true,"base":"USD","date":"2021-02-26","rates":{ "CHF":0.908321,"CNY":6.478899,"EUR":0.828358,"KZT":417.616395,"RUB":74.581492,"VND":23078.093823}}

}
