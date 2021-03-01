using Common;
using Common.Enums;
using Common.EventsArgs;
using Common.Interfaces.Model;
using Common.Interfaces.Repository;
using DtoTypes;
using Repository.Rates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExchangerModels
{
    public partial class ExchangerModel : RatesModel, IMainModel, IExchangerModel
    {

        public decimal BaseCurrencyAmount { get; private set; } = 0;

        public event BaseCurrencyAmountCnahgedHandler BaseCurrencyAmountCnahged;

        /// <summary>  </summary>
        /// <param name="newSumm"> Новая полная сумма. Если было 10 и пользователь внёс ещё 5, то <see cref="newSumm"/> будет 15. </param>
        public void SetNewCurrencySumm(decimal newSumm)
        {
            // TODO : добавить запрос обновления рейтингов ! 
            BaseCurrencyAmount = newSumm;
            // TODO : временно без Обновления списков.
        }

        private IEnumerable<ExchangeDto> RatesToExchenges(IEnumerable<RateDto> rates)
        {
            IEnumerable<ExchangeDto> resultExchenges = new List<ExchangeDto>();

            foreach (var rate in rates)
            {
                decimal availableForExchange = (int)BaseCurrencyAmount / rate.Rate;

                decimal lack;
                if (BaseCurrencyAmount > rate.Rate)
                    lack = BaseCurrencyAmount - (int)rate.Rate * availableForExchange + (rate.Rate - (int)rate.Rate);
                else
                    lack = rate.Rate - BaseCurrencyAmount;

                resultExchenges.Append(new ExchangeDto(rate, BaseCurrencyAmount, availableForExchange, lack));
            }
            return resultExchenges;
        }

        public ExchangerModel(IRatesRepository repository)
            : base(repository)
        {
            exchenges = RatesToExchenges(this.repository.GetCurrentRates()).ToList();

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

        public void SetBaseCurrencyAmount(decimal amount)
        {

            BaseCurrencyAmount = amount;
            BaseCurrencyAmountCnahged?.Invoke(this, BaseCurrencyAmount);

            throw new NotImplementedException();
        }
    }
}
