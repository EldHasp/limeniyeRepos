using ExchangerViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace ExchangerViewModels.Interfaces
{
    public interface IExchangeListViewModel : ISelectedExchangeViewModel
    {
        ObservableCollection<IExchangerViewModel> ExchangeList { get; }
    }
}