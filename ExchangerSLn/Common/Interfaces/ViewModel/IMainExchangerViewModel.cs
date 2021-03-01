namespace Common.Interfaces.ViewModel
{

    /// <summary>Главная ViewModel программы.</summary>
    public interface IMainExchangerViewModel
    {
        IRatesViewModel RatesVM { get; }
        IExchegerViewModel ExchegerVM { get; }
    }
}
