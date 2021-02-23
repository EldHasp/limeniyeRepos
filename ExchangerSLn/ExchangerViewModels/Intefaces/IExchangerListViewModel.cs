using ExchangerViewModels.Intefaces;
using System.Collections.ObjectModel;

namespace ExchangerViewModels.Interfaces
{
    public interface IExchangerListViewModel : ISelectedExchangeViewModel
    {
        ObservableCollection<IExchangeViewModel> Exchanges { get; }
    }
}