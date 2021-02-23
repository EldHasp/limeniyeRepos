using ExchangerViewModels.Interfaces;

namespace ExchangerViewModels.Interfaces
{
    public interface ISelectedExchangeViewModel
    {
        IExchangerViewModel SelectedExchange { get; }
    }
}