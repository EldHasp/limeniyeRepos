using DtoTypes;
using ExchangerModels.API;
using ExchangerModels.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExchangerModels
{
    public partial class ExchangerModel : IExchangerModel
    {
        private  ICurrencyRateAPI api;
       
        public ExchangeDto GetExchange(CurrencyDto currency, CurrencyDto @base, decimal amounBase)
        {
            RateDto pair = api.GetCurrencyRate(currency, @base).Result;
            var value = pair.Rate * amounBase;
            ExchangeDto exchange = new ExchangeDto(pair, amounBase, value, 999);
            return exchange;
        }

        public IReadOnlyList<CurrencyDto> GetCurrencies()
        {
            return availableCurrency;
        }

        ExchangerModel()
        {
            // Инициализация словаря и его неизменяемой оболочки.
            // Для исходного словаря задаём сравнение строк без учёта регистра
            exchanges = new Dictionary<RateDto, ExchangeDto>((IEqualityComparer<RateDto>)StringComparer.CurrentCultureIgnoreCase);
            Exchanges = new ReadOnlyDictionary<RateDto, ExchangeDto>(exchanges);

            api = new CurrencyExchangerate();
        }
    }
}
