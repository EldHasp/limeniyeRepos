using ExchangerViewModels.Intefaces;

namespace ExchangerViewModels.Interfaces
{
    public interface ISelectedExchangeViewModel
    {
        IExchangeViewModel SelectedExchange { get; }
    }
}