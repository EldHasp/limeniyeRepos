using DtoTypes;

namespace Common.Interfaces.ViewModel
{
    /// <summary>Каждый доступный для обмена лот.</summary>
    public interface IExchegerViewModel
    {
        IExchangerListViewModel ExchangeList { get; }
        CurrencyDto BaseCurrency { get; }
        decimal AmounBase { get; }
    }
}
