using Common.Enums;
using DtoTypes;
using System.Collections.Generic;

namespace Common.EventsArgs
{
    /// <summary>Делегат события извещающего об изменении коллекции курсов.</summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="action">Действие изменения.</param>
    /// <param name="newRates">Изменившиеся курсы.</param>
    public delegate void RatesCnahgedHandler(object sender, ChangedAction action, IEnumerable<RateDto> newRates);

    /// <summary>Делегат события извещающего об изменении коллекции вариантов обмена.</summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="action">Действие изменения.</param>
    /// <param name="newExchanges">Изменившиеся варианты</param>
    public delegate void ExchangesCnahgedHandler(object sender, ChangedAction action, IEnumerable<ExchangeDto> newExchanges);

    /// <summary>Делегат события извещающего об изменении базовой валюты.</summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="newBaseCurrency">Новая базовая валюта.</param>
    public delegate void BaseCurrencyCnahgedHandler(object sender, CurrencyDto newBaseCurrency);


    /// <summary>Делегат события извещающего об изменении обменниваемой суммы базовой валюты.</summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="newBaseCurrency">Новая сумма базовая валюта.</param>
    public delegate void BaseCurrencyAmountCnahgedHandler(object sender, decimal newAmount);
}
