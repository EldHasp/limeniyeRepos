using DtoTypes;
using Simplified;
using System.Collections.ObjectModel;

namespace Common.Interface
{

    /// <summary>ViewModel обменика.</summary>
    public interface IExchangerViewModel
    {
        /// <summary>Базовая валюта.</summary>
        CurrencyDto BaseCurrency { get; }

        /// <summary>Обмениваемое количество базовой валюты.</summary>
        decimal AmounBase { get; }

        /// <summary>Запрос предложений по обмену.</summary>
        RelayCommand<(CurrencyDto BaseCurrency, decimal Amoun)> SetBaseCurrencyCommand { get; }

        /// <summary>Запрос курсов.</summary>
        RelayCommand<CurrencyDto> GetRatesCommand { get; }

        /// <summary>Предложения по обмену.</summary>
        ObservableCollection<ExchangeDto> Exchanges { get; }

        /// <summary>Все валюты.</summary>
        ObservableCollection<CurrencyDto> Currencies { get; }

        /// <summary>Все курсы.</summary>
        ObservableCollection<RateDto> Rates{ get; }
    }
}
