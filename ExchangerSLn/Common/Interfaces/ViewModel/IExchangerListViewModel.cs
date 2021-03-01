using DtoTypes;
using System.Collections.ObjectModel;

namespace Common.Interfaces.ViewModel
{
    public interface IExchangerListViewModel : ISelectedExchangePairViewModel
    {
        ObservableCollection<ExchangeDto> Exchanges { get; }
    }
}