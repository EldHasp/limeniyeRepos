using Common.Interfaces.Repository;
using DtoTypes;
using ExchangerModels;
using Repository.Rates;
using System.Collections.Generic;
using System.Windows;

namespace Exchanger
{
    public partial class App : Application
    {
        private RatesRepository repository;
        private ExchangerModel model;

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

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            model = new ExchangerModel(repository = new RatesRepository(1.002m, available, new CurrencyDto("UAH", "₴")));
        }

    }
}
