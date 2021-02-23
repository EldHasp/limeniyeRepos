using System.Collections.ObjectModel;

namespace Common.Interfaces.ViewModel
{
    public interface IExchangerListViewModel : ISelectedExchangeViewModel
    {
        ObservableCollection<IExchangeViewModel> Exchanges { get; }
    }
}