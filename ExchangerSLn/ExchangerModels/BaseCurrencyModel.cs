using Common.EventsArgs;
using Common.Interfaces.Model;
using Common.Interfaces.Repository;
using DtoTypes;

namespace ExchangerModels
{
    public abstract class BaseCurrencyModel : IBaseCurrencyModel
    {
        protected readonly IRatesRepository repository;
        protected BaseCurrencyModel(IRatesRepository repository)
        {
            this.repository = repository;
        }
        public CurrencyDto BaseCurrency { get; private set; }

        public event BaseCurrencyCnahgedHandler BaseCurrencyCnahged;

        public void SetBaseCurrency(CurrencyDto baseCurrency)
        {
            if (BaseCurrency != baseCurrency)
            {
                BaseCurrency = baseCurrency;
                BaseCurrencyCnahged?.Invoke(this, BaseCurrency);
            }
            repository.SetBaseCurrency(BaseCurrency);
        }
    }
}
