using Common.Interfaces.Repository;
using DtoTypes;
using Exchanger.Windows;
using ExchangerModels;
using Repository.Rates;
using System.Collections.Generic;
using System.Windows;
using ViewModel.Currency;

namespace Exchanger
{
    public partial class App : Application
    {
        private IRatesRepository repository;
        private RatesViewModel ratesViewModel;


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
            repository = new RatesRepository(1.002m, available, new CurrencyDto("UAH", "₴"));

            ratesViewModel = new RatesViewModel(repository);

            MainWindow = new MainWindow() { DataContext = ratesViewModel };

            MainWindow.Show();

            //ratesViewModel.Load();
        }

    }
}
