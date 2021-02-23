using Common.Events;
using DtoTypes;
using System;

namespace ExchangerModels
{
    public partial class ExchangerModel
    {
        /// <summary>Событие извещающее об изменении словаря.</summary>
        public EventHandler<NotifyDictionaryChangedEventArgs<string, RateDto>> ChangedCurrencies;
        #region Event Methods RateDto
        /// <summary>Метод установки значения в словаре.</summary>
        /// <param name="exchange">Пара валют и её курс.</param>
        /// <remarks>Если валюты нет - она добавляется. Если есть, то заменяется значение.</remarks>
        private void SetCurrencyValue(ExchangeDto exchange)
        {
            if (exchanges.TryGetValue(exchange.Rate, out ExchangeDto oldCurrency))
            {
                exchanges[exchange.Rate] = exchange;
                ChangedCurrencies?.Invoke(Exchanges, NotifyActionDictionaryChangedEventArgs.ChangedValue(exchange.Rate, oldCurrency, exchange));
            }
            else
            {
                exchanges.Add(exchange.Rate, exchange);
                ChangedCurrencies?.Invoke(Exchanges, NotifyActionDictionaryChangedEventArgs.AddKey(exchange.Rate, exchange));
            }
        }

        /// <summary>Метод удаления валюты из списка.</summary>
        /// <param name="type">Тип валюты.</param>
        private void RemoveCurrency(RateDto type)
        {
            if (exchanges.TryGetValue(type, out RateDto oldCurrency))
            {
                exchanges.Remove(type);
                ChangedCurrencies?.Invoke(Exchanges, NotifyActionDictionaryChangedEventArgs.RemoveKey(type, oldCurrency));
            }
        }
        #endregion

    }
}
