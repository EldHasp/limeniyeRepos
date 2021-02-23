using DtoTypes;
using Simplified;
using System.Collections.ObjectModel;

namespace ExchangerViewModels.Interfaces
{

    /// <summary>ViewModel обменика.</summary>
    public interface IMainExchangerViewModel : IExchangerListViewModel
    {
        /// <summary>Базовая валюта.</summary>
        CurrencyDto BaseCurrency { get; }

        /// <summary>Обмениваемое количество базовой валюты.</summary>
        decimal AmounBase { get; }

        /// <summary>Запрос предложений по обмену.</summary>
        RelayCommand<CurrencyDto> SetBaseCurrencyCommand { get; }

        /// <summary>Все валюты.</summary>
        ObservableCollection<CurrencyDto> Currencies { get; }

    }
}
