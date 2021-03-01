using Common;
using Common.Enums;
using Common.EventsArgs;
using DtoTypes;
using System;
using System.Collections.Generic;

namespace Repository.Rates
{
    public partial class RatesRepository
    {
        #region Событие и методы изменения rates
        /// <summary>Событие извещающее о изменении производных курсов.
        public event RatesCnahgedHandler RatesCnahged;

        /// <summary>Приватный метод очистки производных курсов.</summary>
        private void ClearRates()
        {
            rates.Clear();
            RatesCnahged?.Invoke(this, RatesAction.Clear, null);
        }

        /// <summary>Добавление или замена одного производного курса.</summary>
        /// <param name="addRate">Новый или изменённый курс.</param>
        private void AddRates(RateDto addRate)
        {
            rates.ReplaceOrAdd(r => r.Base == addRate.Base && r.Currency == addRate.Currency, addRate);
            RatesCnahged?.Invoke(this, RatesAction.AddedOrChanged, new RateDto[] { addRate });
        }

        /// <summary>Добавление или замена нескольких производных курсов.</summary>
        /// <param name="addRate">Новые или изменённые курсы.</param>
        private void AddRangeRates(IEnumerable<RateDto> addRates)
        {
            foreach (var rate in addRates)
                rates.ReplaceOrAdd(r => r.Base == rate.Base && r.Currency == rate.Currency, rate);

            RatesCnahged?.Invoke(this, RatesAction.AddedOrChanged, addRates.GetEnumerable());
        }
        #endregion
    }
}
