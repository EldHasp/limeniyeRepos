using DtoTypes;
using Repository.Rates;
using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        private static IList<CurrencyDto> available { get; } = new List<CurrencyDto>()
        {
            new CurrencyDto("USD", "$"),
            new CurrencyDto("EUR", "€"),
            new CurrencyDto("RUB", "₽"),
            new CurrencyDto("CNY", "¥"),
            new CurrencyDto("KZT", "₸"),
            new CurrencyDto("VND", "₫"),
            new CurrencyDto("CHF", "₣")
        };
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            RatesRepository exchangeRate = new RatesRepository();
            var rate = await exchangeRate.GetRateOfCurrencyAsync(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"));
            var rates = await exchangeRate.GetAllRatesOfCurrencyAsync(new CurrencyDto("UAH", "₴"), available);
            Console.ReadLine();
        }
    }
}
