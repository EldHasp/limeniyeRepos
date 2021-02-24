using Common.Events;
using DtoTypes;
using System;

namespace Repository.ExchangerateRepository
{
    public partial class ExchangerateRepository
    {
        /// <summary>Событие извещающее об изменении словаря.</summary>
        public EventHandler<NotifyDictionaryChangedEventArgs<int, RateDto>> ChangedExchanges { get; set; }
        #region Event Methods RateDto
        /// <summary>Метод установки значения в словаре.</summary>
        /// <param name="newRate">Пара валют и её курс.</param>
        /// <remarks>Если валюты нет - она добавляется. Если есть, то заменяется значение.</remarks>
        private void SetRateValue(RateDto newRate)
        {
            if (rates.TryGetValue(newRate.Id, out RateDto oldCurrency))
            {
                rates[newRate.Id] = newRate;
                ChangedExchanges?.Invoke(Rates, NotifyActionDictionaryChangedEventArgs.ChangedValue(newRate.Id, oldCurrency, newRate));
            }
            else
            {
                rates.Add(newRate.Id, newRate);
                ChangedExchanges?.Invoke(Rates, NotifyActionDictionaryChangedEventArgs.AddKey(newRate.Id, newRate));
            }
        }

        private void RemoveCurrency(int id)
        {
            if (rates.TryGetValue(id, out RateDto oldCurrency))
            {
                rates.Remove(id);
                ChangedExchanges?.Invoke(Rates, NotifyActionDictionaryChangedEventArgs.RemoveKey(id, oldCurrency));
            }
        }
        #endregion
    }
}
