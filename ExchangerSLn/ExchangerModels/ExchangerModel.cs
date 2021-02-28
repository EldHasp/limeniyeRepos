using Common;
using DtoTypes;
using Repository.Rates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExchangerModels
{
    public partial class ExchangerModel
    {
        private readonly RatesRepository repository;
        private readonly IList<RateDto> rates;
        private readonly IList<ExchangeDto> exchangers = new List<ExchangeDto>();


        /// <summary>  </summary>
        /// <param name="newSumm"> Новая полная сумма. Если пбыло 10 и пользователь внёс ещё 5, то <see cref="newSumm"/> будет 15. </param>
        public void SetDepositedBalance(decimal newSumm)
        {
            for (int i = 0; i < exchangers.Count; i++)
            {
                decimal availableForExchange = (int)newSumm / exchangers[i].Rate.Rate;
                decimal lack;
                if (newSumm > exchangers[i].Rate.Rate)
                    lack = newSumm - (int)exchangers[i].Rate.Rate * availableForExchange + (exchangers[i].Rate.Rate - (int)exchangers[i].Rate.Rate);
                else
                    lack = exchangers[i].Rate.Rate - newSumm;

                exchangers[i] = new ExchangeDto(exchangers[i].Rate, newSumm, availableForExchange, lack);
            }
        }


        public ExchangerModel(RatesRepository repository)
        {
            this.repository = repository;
            rates = this.repository.GetCurrentRates().ToArray();
            //первичное заполнение exchangers с эмуляцией внесения 30 грн.
            decimal newSumm = 30;
            
            foreach (var item in rates)
            {
                decimal availableForExchange = (int)newSumm / item.Rate;
                decimal lack;
                if (newSumm > item.Rate)
                    lack = newSumm - (int)item.Rate * availableForExchange + (item.Rate - (int)item.Rate);
                else
                    lack = item.Rate - newSumm;

                exchangers.Add(new ExchangeDto(item, newSumm, availableForExchange, lack));
            }

            this.repository.RatesCnahged += Repository_RatesCnahged;
        }

        
        private void Repository_RatesCnahged(object sender, Common.EventsArgs.RatesAction action, System.Collections.Generic.IEnumerable<DtoTypes.RateDto> newRates)
        {
            switch (action)
            {
                case Common.EventsArgs.RatesAction.AddedOrChanged:
                    foreach (var rate in newRates)
                        rates.ReplaceOrAdd(r => r.Base == rate.Base && r.Currency == rate.Currency, rate);
                    break;
                case Common.EventsArgs.RatesAction.Clear:
                    rates.Clear();
                    break;
            }
        }
    }
}
