using Common.Enums;
using Common.EventsArgs;
using DtoTypes;
using System;

namespace ExchangerModels
{
    public partial class ExchangerModel
    {
        /// <summary>Событие извещающее об изменении словаря.</summary>
        public EventHandler<NotifyDictionaryChangedEventArgs<int, ExchangeDto>> ChangedExchanges { get; set; }
        public EventHandler<NotifyDictionaryChangedEventArgs<int, RateDto>> ChangedRates { get; set; }
        #region Event Methods ExchangeDto
        /// <summary>Метод установки значения в словаре.</summary>
        /// <param name="exchange">Пара валют и её курс.</param>
        /// <remarks>Если валюты нет - она добавляется. Если есть, то заменяется значение.</remarks>
        private void SetExchangeValue(ExchangeDto exchange)
        {
            if (exchanges.TryGetValue(exchange.Id, out ExchangeDto oldCurrency))
            {
                exchanges[exchange.Id] = exchange;
                ChangedExchanges?.Invoke(Exchanges, NotifyActionDictionaryChangedEventArgs.ChangedValue(exchange.Id, oldCurrency, exchange));
            }
            else
            {
                exchanges.Add(exchange.Id, exchange);
                ChangedExchanges?.Invoke(Exchanges, NotifyActionDictionaryChangedEventArgs.AddKey(exchange.Id, exchange));
            }
        }

        /// <summary>Метод удаления валюты из списка.</summary>
        /// <param name="type">Тип валюты.</param>
        private void RemoveCurrency(int id)
        {
            if (exchanges.TryGetValue(id, out ExchangeDto oldCurrency))
            {
                exchanges.Remove(id);
                ChangedExchanges?.Invoke(Exchanges, NotifyActionDictionaryChangedEventArgs.RemoveKey(id, oldCurrency));
            }
        }
        #endregion


        #region Event Methods RateDto
        /// <summary>Метод установки значения в словаре.</summary>
        /// <param name="rate">Пара валют и её курс.</param>
        /// <remarks>Если валюты нет - она добавляется. Если есть, то заменяется значение.</remarks>
        private void SetRateValue(RateDto rate)
        {
            if (rates.TryGetValue(rate.Id, out RateDto oldRate))
            {
                rates[rate.Id] = rate;
                ChangedRates?.Invoke(Exchanges, NotifyActionDictionaryChangedEventArgs.ChangedValue(rate.Id, oldRate, rate));
            }
            else
            {
                rates.Add(rate.Id, rate);
                ChangedRates?.Invoke(Exchanges, NotifyActionDictionaryChangedEventArgs.AddKey(rate.Id, rate));
            }
        }

        /// <summary>Метод удаления валюты из списка.</summary>
        /// <param name="type">Тип валюты.</param>
        private void RemoveRate(int id)
        {
            if (rates.TryGetValue(id, out RateDto oldRate))
            {
                rates.Remove(id);
                ChangedRates?.Invoke(Exchanges, NotifyActionDictionaryChangedEventArgs.RemoveKey(id, oldRate));
            }
        }
        #endregion
    }

}
