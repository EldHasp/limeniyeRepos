using Common.Interfaces.Model;
using DtoTypes;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ExchangerModels
{
    public partial class ExchangerModel : IExchangerModel
    {
        private IRepository<RateDto> api;
        private CurrencyDto Base;
        
        public Task SetBaseCurrency(CurrencyDto currency)
        {
            Base = currency;
            bool result = false;
            UpdateRates();
            UpdateExchangeRates();
            return null; 
        }


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

        public Task UpdateRates()
        {
            return null;
        }
        public Task UpdateExchangeRates()
        {
            return null;
        }

        private async void UpdateRates(int everySeconds, IList<CurrencyDto> availbleCurrenciew, CurrencyDto @base)
        {
            while (true)
            {
                var task = Task.Run(() => api.GetAllCurrencyRate(@base, availbleCurrenciew));
                try
                {
                    var ratesResult = await task;
                    foreach (var item in ratesResult)
                    {
                        if (rates.Find(x => x.Currency == item.Currency && x.Base == item.Base) != null)
                            rates.Add(item);
                        //else
                        //rates.Find(x => x.Currency == item.Currency && x.Base == item.Base ) = item;\

                        RaiseActionRates(Common.Enums.ActionListEnum.Added, item);
                    }
                    
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                await Task.Delay(everySeconds * 1000);
            }
        }

        public ExchangerModel()
        {
            // Инициализация словаря и его неизменяемой оболочки.
            // Для исходного словаря задаём сравнение строк без учёта регистра
            exchanges = new Dictionary<RateDto, ExchangeDto>((IEqualityComparer<RateDto>)StringComparer.CurrentCultureIgnoreCase);
            Exchanges = new ReadOnlyDictionary<RateDto, ExchangeDto>(exchanges);



            api = new CurrencyExchangerate();


            UpdateRates(600, (IList<CurrencyDto>)availableCurrency, Base);
        }
    }
}
