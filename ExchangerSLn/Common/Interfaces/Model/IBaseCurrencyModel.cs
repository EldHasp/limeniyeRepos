using Common.EventsArgs;
using DtoTypes;

namespace Common.Interfaces.Model
{
    /// <summary>Интерфейс модели, задающей базовую валюту.</summary>
    public interface IBaseCurrencyModel
    {
        /// <summary>Событие извещающее об изменении <see cref="BaseCurrency"/>.</summary>
        event BaseCurrencyCnahgedHandler BaseCurrencyCnahged;

        /// <summary>Базовая валюта.</summary>
        CurrencyDto BaseCurrency { get; }

        /// <summary>Задание базовой валюты.</summary>
        void SetBaseCurrency(CurrencyDto baseCurrency);
    }

}
