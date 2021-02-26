using Common.Interfaces.Repository;
using DtoTypes;
using Repository.Rates;
using System;
using System.Collections.Generic;
using System.Linq;

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
            RatesRepository exchangeRate = new RatesRepository(1.002m, available);

            exchangeRate.RatesCnahged += ExchangeRate_RatesCnahged;
            ((ISupportInitializeRatesRepository)exchangeRate).Initialize(available, new CurrencyDto("UAH", "₴"));
            Console.ReadKey();
        }

        private static void ExchangeRate_RatesCnahged(object sender, Common.EventsArgs.RatesAction action, IEnumerable<RateDto> newRates)
        {
            switch (action)
            {
                case Common.EventsArgs.RatesAction.AddedOrChanged:
                    Console.WriteLine($"Изменились или добавились новые значения - {DateTime.Now}:");
                    Console.WriteLine(string.Join(Environment.NewLine, newRates));
                    Console.WriteLine();
                    break;
                case Common.EventsArgs.RatesAction.Clear:
                    Console.WriteLine("\tСписок очистился.\t");
                    break;
            }
        }
    }
}
