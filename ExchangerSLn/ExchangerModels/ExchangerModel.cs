using Common;
using Common.Enums;
using Common.EventsArgs;
using Common.Interfaces.Model;
using Common.Interfaces.Repository;
using DtoTypes;
using Repository.Rates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExchangerModels
{
    public partial class ExchangerModel : RatesModel, IMainModel
    {

        public decimal BaseCurrencyAmount { get; private set; } = 0;

        public event BaseCurrencyAmountCnahgedHandler BaseCurrencyAmountCnahged;

        private IEnumerable<ExchangeDto> RatesToExchenges(IEnumerable<RateDto> rates)
        {
            IList<ExchangeDto> resultExchenges = new List<ExchangeDto>();

            foreach (var rate in rates)
            {
                decimal availableForExchange = (int)(BaseCurrencyAmount / rate.Rate);

                decimal lack;

                decimal mustBe = rate.Rate * (availableForExchange + 1);
                decimal test1 = (int)rate.Rate * availableForExchange;
                decimal test2 = rate.Rate - (int)rate.Rate;

                if (BaseCurrencyAmount > rate.Rate)
                    lack = mustBe - (BaseCurrencyAmount);
                else
                    lack = rate.Rate - BaseCurrencyAmount;

                resultExchenges.Add(new ExchangeDto(rate, BaseCurrencyAmount, availableForExchange, lack));
            }
            return resultExchenges;
        }


        public void SetBaseCurrencyAmount(decimal amount)
        {
            BaseCurrencyAmount = amount;

            AddRangeExchenges(RatesToExchenges(this.repository.GetCurrentRates()).ToList());

            BaseCurrencyAmountCnahged?.Invoke(this, BaseCurrencyAmount);
        }


        public ExchangerModel(IRatesRepository repository): base(repository)
        {
            exchenges = RatesToExchenges(this.repository.GetCurrentRates()).ToList();
            Exchanges = new ReadOnlyCollection<ExchangeDto>(exchenges);
            this.repository.RatesCnahged += OnRatesCnahged;
        }

        private void OnRatesCnahged(object sender, ChangedAction action, IEnumerable<RateDto> newRates)
        {
            switch (action)
            {
                case ChangedAction.AddedOrChanged:
                    AddRangeExchenges(RatesToExchenges(newRates));
                    break;
                case ChangedAction.Clear:
                    ClearExchenges();
                    break;
            }
        }

      
    }
}
