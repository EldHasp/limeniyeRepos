using DtoTypes;

namespace Common.Interfaces.ViewModel
{
    /// <summary>Каждый доступный для обмена лот.</summary>
    public interface IExchegerViewModel : IExchangerListViewModel
    {
        CurrencyDto BaseCurrency { get; }
        decimal AmounBase { get; }
    }
}
