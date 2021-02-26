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


        static void Main(string[] args)
        {

            RatesRepository exchangeRate = new RatesRepository(new CurrencyDto("UAH", "₴") , available);
            foreach (var item in exchangeRate.GetAllRates())
            {
                Console.WriteLine("Базовая валюта -- " + item.Base + "\tПобочная -- " + item.Currency + "\tКурс -- " + item.Rate);
            }
            exchangeRate.RatesCnahged += ExchangeRate_RatesCnahged;

            //var rate = await exchangeRate.GetRateOfCurrencyAsync(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"));
            //var rates = await exchangeRate.GetAllRatesOfCurrencyAsync(new CurrencyDto("UAH", "₴"), available);
            Console.ReadLine();
        }

        private static void ExchangeRate_RatesCnahged(object sender, Common.EventsArgs.RatesAction action, IEnumerable<RateDto> newRates)
        {
            switch(action)
            {
                case Common.EventsArgs.RatesAction.AddedOrChanged:
                    Console.WriteLine("Изменились или добавились новые значения\n");
                    foreach (var item in newRates)
                    {
                        Console.WriteLine("Базовая валюта -- " + item.Base + "\tПобочная -- " + item.Currency + "\tКурс -- " + item.Rate);
                    }
                    break;
                case Common.EventsArgs.RatesAction.Clear:
                    Console.WriteLine("\tСписок очистился.\t");
                    break;
            }
        }
    }
}
