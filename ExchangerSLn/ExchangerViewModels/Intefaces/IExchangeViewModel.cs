using DtoTypes;

namespace ExchangerViewModels.Intefaces
{
    /// <summary>Каждый доступный для обмена лот.</summary>
    public interface IExchangeViewModel
    {
        ExchangeDto ExchangeDto { get; }
        void SetNewExchange(ExchangeDto rate);
    }
}
