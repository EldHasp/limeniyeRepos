using Common.EventsArgs;
using DtoTypes;
using System;

namespace Repository.Rates
{
    public partial class RatesRepository
    {
        public event EventHandler<NotifyCollectionChangedEventArgs<RateDto>> ChangedRates;
        #region Event Methods RateDto
        private void SetRateValue(RateDto newRate)
        {
            // TODO : Могут быть проблемы если список изменится во время запуска алгоритма.
            
            
            int index = rates.IndexOf(rates.Find(x => x.Id == newRate.Id));
            RateDto oldRate = rates[index];
            if (oldRate != null)
            {
                rates[index] = newRate;
                ChangedRates?.Invoke(rates, NotifyRatesChangedEventArgs<RateDto>.Changed(newRate.Id, oldCurrency, newRate));
            }
            else
            {
                rates.Add(newRate.Id, newRate);
                ChangedExchanges?.Invoke(Rates, NotifyCollectionChangedEventArgs.Added(newRate.Id, newRate));
            }
        }
        #endregion
    }
}
