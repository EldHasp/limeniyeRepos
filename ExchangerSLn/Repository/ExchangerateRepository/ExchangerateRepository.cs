using Common.Enums;
using Common.Interfaces.Repository;
using DtoTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using xNet;

namespace Repository.ExchangerateRepository
{
    public partial class ExchangerateRepository : IRatesRepository
    {
        /// <summary>Приватный изменяемый словарь курсов.</summary>
        private readonly Dictionary<int, RateDto> rates;

        /// <summary>Публичный неизменяемый словарь курсов.</summary>
        public ReadOnlyDictionary<int, RateDto> Rates { get; }

        private ReadOnlyDictionary<string, CurrencyDto> available { get; }
            = new ReadOnlyDictionary<string, CurrencyDto>
            (new CurrencyDto[]
                {
                    new CurrencyDto("USD", "$"),
                    new CurrencyDto("EUR", "€"),
                    new CurrencyDto("RUB", "₽"),
                    new CurrencyDto("CNY", "¥"),
                    new CurrencyDto("KZT", "₸"),
                    new CurrencyDto("VND", "₫"),
                    new CurrencyDto("CHF", "₣")
                }
                .ToDictionary(p => p.Symbol)
            );

        public ExchangerateRepository()
        {
            // Инициализация словаря и его неизменяемой оболочки.
            // Для исходного словаря задаём сравнение строк без учёта регистра
            rates = new Dictionary<int, RateDto>((IEqualityComparer<int>)StringComparer.CurrentCultureIgnoreCase);
            Rates = new ReadOnlyDictionary<int, RateDto>(rates);
        }

        public async Task GetCurrencyRate(CurrencyDto currency, CurrencyDto @base)
        {
            HttpRequest request = new HttpRequest();
            RequestParams rParams = new RequestParams();
            rParams["base"] = @base.Symbol;
            rParams["symbols"] = currency.Symbol;
            rParams["format"] = "XML";

            HttpResponse response = await Task.Run(() => request.Get("https://api.exchangerate.host/latest", rParams));
            XDocument doc = XDocument.Parse(response.ToString());
            XElement element = doc.Descendants("data").First();
            decimal rate = element.Descendants("rate").Select(v => (decimal)v).FirstOrDefault();
            RateDto result = new RateDto(@base, currency, rate);
            SetRateValue(result);
        }

        public async Task GetAllCurrencyRate(CurrencyDto @base)
        {
            HttpRequest request = new HttpRequest();
            RequestParams rParams = new RequestParams();
            rParams["base"] = @base.Symbol;
            rParams["format"] = "XML";
            HttpResponse response = await Task.Run(() => request.Get("https://api.exchangerate.host/latest", rParams));
            XDocument doc = XDocument.Parse(response.ToString());
            var elements = doc.Descendants("data");


            foreach (var item in elements)
            {
                decimal rate = item.Descendants("rate").Select(v => (decimal)v).FirstOrDefault();
                string currencyName = item.Descendants("code").Select(v => (string)v).FirstOrDefault();
                SetRateValue(new RateDto(available[currencyName], @base, rate));               
            }
        }
    }
}
