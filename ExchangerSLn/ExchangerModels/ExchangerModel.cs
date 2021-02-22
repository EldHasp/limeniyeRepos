using Common.Interface;
using DtoTypes;
using System;
using System.Collections.Generic;

namespace ExchangerModels
{
    public class ExchangerModel : IExchangerModel
    {
        public IReadOnlyList<CurrencyDto> GetCurrencies()
        {
            throw new NotImplementedException();
        }

        public ExchangeDto GetExchange(CurrencyDto currency, CurrencyDto @base, decimal amounBase)
        {
            throw new NotImplementedException();
        }

        public RateDto GetRate(CurrencyDto currency, CurrencyDto @base)
        {
            throw new NotImplementedException();
        }
    }
}
