using Common.EventsArgs;
using DtoTypes;
using System.Collections.Generic;

namespace Common.Interfaces.Model
{
    /// <summary>Интерфейс модели обменика.</summary>
    public interface IExchangerModel : IBaseCurrencyModel
    {
        /// <summary>Коллекция вариантов обмена суммы в базовой валюте по отношению к <see cref="BaseCurrency"/>.</summary>
        IReadOnlyCollection<ExchangeDto> Exchanges { get; }

        /// <summary>Событие извещающее об изменении <see cref="Exchanges"/>.</summary>
        event ExchangesCnahgedHandler ExchangesCnahged;

        /// <summary>Количество базовой ваалюты предлагаемой для обмена.</summary>
        decimal BaseCurrencyAmount { get; }

        /// <summary>Событие извещающее об изменении <see cref="BaseCurrencyAmount"/>.</summary>
        event BaseCurrencyAmountCnahgedHandler BaseCurrencyAmountCnahged;

        /// <summary>Метод изменения <see cref="BaseCurrencyAmount"/>.</summary>
        /// <param name="amount">Количество базовой валюты предлагаемой для обмена.</param>
        void SetBaseCurrencyAmount(decimal amount);
    }

}
