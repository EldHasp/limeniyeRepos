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

            Console.WriteLine("Выберите вариант инициализации:");
            Console.WriteLine("1 - Перврначальный вариант с иницализацией через конструтор;");
            Console.WriteLine("2 - Второй вариант с иницализацией через транзакцию инициализации;");
            Console.WriteLine("3 - Третий вариан вариант с иницализацией через конструтор с передачей ему прослушки;");
            ConsoleKeyInfo key;
            while (!"123".Contains((key = Console.ReadKey()).KeyChar))
                Console.Write('\r');
            Console.WriteLine();
            Console.WriteLine();

            if (key.KeyChar == '1')
            {
                // Перврначальный вариант с иницализацией через конструтор.
                RatesRepository exchangeRate = new RatesRepository(1.002m, available, new CurrencyDto("UAH", "₴"));

                exchangeRate.RatesCnahged += ExchangeRate_RatesCnahged;
            }
            else if (key.KeyChar == '2')
            {
                // Второй вариант с иницализацией через транзакцию инициализации.
                RatesRepository exchangeRate = new RatesRepository(1.002m);

                exchangeRate.RatesCnahged += ExchangeRate_RatesCnahged;

                ((ISupportInitializeRatesRepository)exchangeRate).Initialize(available, new CurrencyDto("UAH", "₴"));
            }
            else
            {
                // Третий вариан вариант с иницализацией через конструтор с передачей ему прослушки.
                RatesRepository exchangeRate = new RatesRepository(1.002m, available, ExchangeRate_RatesCnahged, new CurrencyDto("UAH", "₴"));
                Console.WriteLine("Данные после инициализации: ");
                Console.WriteLine(string.Join(Environment.NewLine, exchangeRate.GetCurrentRates()));
                Console.WriteLine("Данные закончились. \n_________________________\n");
            }
            Console.ReadKey();
        }

        private static void ExchangeRate_RatesCnahged(object sender, Common.EventsArgs.RatesAction action, IEnumerable<RateDto> newRates)
        {
            switch (action)
            {
                case Common.EventsArgs.RatesAction.AddedOrChanged:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Изменились или добавились новые значения - {DateTime.Now}:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(string.Join(Environment.NewLine, newRates));
                    Console.WriteLine();
                    break;
                case Common.EventsArgs.RatesAction.Clear:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tСписок очистился.\t");
                    break;
            }
        }
    }
}
