using DtoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using xNet;

namespace ExchangerModels.API
{
    public class CurrencyExchangerate : ICurrencyRateAPI
    {
        public Task<RateDto> GetCurrencyRate(CurrencyDto currency, CurrencyDto @base)
        {
            HttpRequest request = new HttpRequest();
            RequestParams rParams = new RequestParams();
            rParams["base"] = @base.Symbol;
            rParams["symbols"] = currency.Symbol;
            rParams["format"] = "XML";
            HttpResponse response = request.Get("https://api.exchangerate.host/latest", rParams);
            XDocument doc = XDocument.Parse(response.ToString());
            XElement element = doc.Descendants("data").First();
            decimal rate = element.Descendants("rate").Select(v => (decimal)v).FirstOrDefault();
            RateDto result = new RateDto(@base, currency, rate);
            return Task.Run(() => result);
        }

        public Task<IList<RateDto>> GetAllCurrencyRate(CurrencyDto @base, IList<CurrencyDto> available)
        {
            HttpRequest request = new HttpRequest();
            RequestParams rParams = new RequestParams();
            rParams["base"] = @base.Symbol;
            rParams["format"] = "XML";
            HttpResponse response = request.Get("https://api.exchangerate.host/latest", rParams);
            XDocument doc = XDocument.Parse(response.ToString());
            var elements = doc.Descendants("data");

            IList<RateDto> tempList = new List<RateDto>();
            foreach (var item in elements)
            {
                decimal rate = item.Descendants("rate").Select(v => (decimal)v).FirstOrDefault();
                string currencyName = item.Descendants("code").Select(v => (string)v).FirstOrDefault();
                tempList.Add(new RateDto(available.First(x => x.Symbol == currencyName), @base, rate));
            }

            //decimal rate = Convert.ToDecimal(element.Descendants("rate").Select(v => (string)v).FirstOrDefault());
            //RateDto result = new RateDto(mainCurrency, sideCurrencyForRating, rate);
            return Task.Run(() => tempList);
        }
    }
}
