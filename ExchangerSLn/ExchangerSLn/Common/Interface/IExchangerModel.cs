using DtoTypes;
using System.Collections.Generic;

namespace Common.Interface
{
    /// <summary>Интерфейс обменика.</summary>
    public interface IExchangerModel
    {
        /// <summary>Получить общий список всех доступных валют.</summary>
        /// <returns>Индексированный список только для чтения со всеми доступными валютами.</returns>
        IReadOnlyList<CurrencyDto> GetCurrencies();

        /// <summary>Получение  курса пары.</summary>
        /// <param name="currency">Оцениваемая валюта.</param>
        /// <param name="base">Базовая валюта.</param>
        /// <returns>Курс запрошенной пары.</returns>
        RateDto GetRate(CurrencyDto currency, CurrencyDto @base);

        /// <summary>Получение предложения по обмену базовой валюты.</summary>
        /// <param name="currency">Получаемая валюта.</param>
        /// <param name="base">Базовая валюта.</param>
        /// <param name="amounBase">Обмениваемая сумма базовой валюты.</param>
        /// <returns>Предложение по паре пары.</returns>
        ExchangeDto GetExchange(CurrencyDto currency, CurrencyDto @base, decimal amounBase);
    }
}
