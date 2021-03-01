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
    public partial class ExchangerModel : IExchangerModel
    {
        private readonly IRatesRepository repository;

        public decimal CurrentSumm { get; private set; } = 0;

        /// <summary>  </summary>
        /// <param name="newSumm"> Новая полная сумма. Если было 10 и пользователь внёс ещё 5, то <see cref="newSumm"/> будет 15. </param>
        public void SetNewCurrencySumm(decimal newSumm)
        {
            // TODO : добавить запрос обновления рейтингов ! 
            CurrentSumm = newSumm;
            // TODO : временно без Обновления списков.
        }

        private IEnumerable<ExchangeDto> RatesToExchenges(IEnumerable<RateDto> rates)
        {
            IEnumerable<ExchangeDto> resultExchenges = new List<ExchangeDto>();

            foreach (var rate in rates)
            {
                decimal availableForExchange = (int)CurrentSumm / rate.Rate;

                decimal lack;
                if (CurrentSumm > rate.Rate)
                    lack = CurrentSumm - (int)rate.Rate * availableForExchange + (rate.Rate - (int)rate.Rate);
                else
                    lack = rate.Rate - CurrentSumm;

                resultExchenges.Append(new ExchangeDto(rate, CurrentSumm, availableForExchange, lack));
            }
            return resultExchenges;
        }

        public ExchangerModel(IRatesRepository repository)
        {
            this.repository = repository;
            exchenges = RatesToExchenges(this.repository.GetCurrentRates()).ToList();

            this.repository.RatesCnahged += Repository_RatesCnahged;
        }

        private void Repository_RatesCnahged(object sender, RatesAction action, IEnumerable<RateDto> newRates)
        {
            switch (action)
            {
                case RatesAction.AddedOrChanged:
                    AddRangeExchenges(RatesToExchenges(newRates));
                    break;
                case RatesAction.Clear:
                    ClearExchenges();
                    break;
            }
        }
    }
}
