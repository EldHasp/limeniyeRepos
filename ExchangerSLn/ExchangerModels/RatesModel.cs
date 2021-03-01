using Common;
using Common.Enums;
using Common.EventsArgs;
using Common.Interfaces.Model;
using Common.Interfaces.Repository;
using DtoTypes;
using System.Collections.Generic;

namespace ExchangerModels
{
    public abstract class RatesModel : BaseCurrencyModel, IRatesModel
    {
        protected RatesModel(IRatesRepository repository)
            : base(repository)
        {
            this.repository.RatesCnahged += OnRatesCnahged;
            Rates = rates.AsReadOnly();
        }

        public IReadOnlyList<RateDto> Rates { get; }
        private readonly List<RateDto> rates = new List<RateDto>();


        private void OnRatesCnahged(object sender, ChangedAction action, IEnumerable<RateDto> newRates)
        {
            switch (action)
            {
                case ChangedAction.AddedOrChanged:
                    AddRangeRates(newRates);
                    break;
                case ChangedAction.Clear:
                    ClearRates();
                    break;
            }
        }

        #region Событие и методы изменения Rates
        /// <summary>Событие извещающее о изменении курсов.
        public event RatesCnahgedHandler RatesCnahged;

        /// <summary>Приватный метод очистки производных предложений обмена.</summary>
        private void ClearRates()
        {
            rates.Clear();
            RatesCnahged?.Invoke(this, ChangedAction.Clear, null);
        }

        /// <summary>Добавление или замена одного курса.</summary>
        /// <param name="addRate">Новый или изменённый курс.</param>
        private void AddRate(RateDto addRate)
        {
            rates.ReplaceOrAdd(r => r.Id == addRate.Id, addRate);
            RatesCnahged?.Invoke(this, ChangedAction.AddedOrChanged, new RateDto[] { addRate }.GetEnumerable());
        }

        /// <summary>Добавление или замена нескольких производных предложений обмена.</summary>
        /// <param name="addRate">Новые или изменённые курсы.</param>
        private void AddRangeRates(IEnumerable<RateDto> addRates)
        {
            foreach (RateDto rate in addRates)
                rates.ReplaceOrAdd(r => r.Id == rate.Id, rate);

            RatesCnahged?.Invoke(this, ChangedAction.AddedOrChanged, addRates.GetEnumerable());
        }
        #endregion

    }
}
