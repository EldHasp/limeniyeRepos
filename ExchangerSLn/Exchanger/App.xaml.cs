using Common.Interfaces.Model;
using Common.Interfaces.Repository;
using Common.Interfaces.ViewModel;
using DtoTypes;
using Exchanger.Windows;
using ExchangerModels;
using ExchangerViewModels;
using Repository.Rates;
using System.Collections.Generic;
using System.Windows;
using ViewModel.Currency;

namespace Exchanger
{
    public partial class App : Application
    {
        private IRatesRepository repositoryModel;
        private IExchangerModel exchangerModel;
        private IMainExchangerViewModel mainViewModel;


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
            repositoryModel = new RatesRepository(1.002m, available, new CurrencyDto("UAH", "₴")); //Инициализация репозитория для первой страницы 
            exchangerModel = new ExchangerModel(repositoryModel); //Инициализация exchenger для второй страницы 

            mainViewModel = new MainViewModel(repositoryModel, exchangerModel);

            MainWindow = new MainWindow() { DataContext = mainViewModel };

            MainWindow.Show();
        }

    }
}
