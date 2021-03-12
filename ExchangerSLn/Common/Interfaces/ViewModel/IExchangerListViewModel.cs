using Common.EventsArgs;
using System.Collections.ObjectModel;

namespace Common.Interfaces.ViewModel
{
    public interface IExchangerListViewModel : ISelectedExchangePairViewModel
    {
        int Rows { get; }
        int Columns { get; }
        ObservableCollection<ICellViewModel> Page { get; }

        /// <summary>Событие извещающее об изменении <see cref="BaseCurrencyAmount"/>.</summary>
        event ExchangeSelectedHandler ExchangeSelected;
    }
}