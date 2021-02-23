using DtoTypes;
using Simplified;
using System.Collections.ObjectModel;

namespace ExchangerViewModels.Interfaces
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

        /// <summary>Предложения по обмену.</summary>
        ObservableCollection<ExchangeDto> Exchanges { get; }

        /// <summary>Все валюты.</summary>
        ObservableCollection<CurrencyDto> Currencies { get; }

    }
}
