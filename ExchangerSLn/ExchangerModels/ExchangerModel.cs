using Common.Interfaces.Model;
using Common.Interfaces.Repository;
using DtoTypes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ExchangerModels
{
    public partial class ExchangerModel : IExchangerModel
    {
        private IRatesRepository api;
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
            //RateDto pair = api.GetCurrencyRate(currency, @base).Result;
            //var value = pair.Rate * amounBase;
            //ExchangeDto exchange = new ExchangeDto(pair, amounBase, value, 999);
            return null;
        }

        public IReadOnlyList<CurrencyDto> GetCurrencies()
        {
            return null;
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
           
        }

        public ExchangerModel()
        {
            exchanges = new Dictionary<int, ExchangeDto>();
            Exchanges = new ReadOnlyDictionary<int, ExchangeDto>(exchanges);

            api = new ExchangerateRepository();


            //UpdateRates(600, (IList<CurrencyDto>)availableCurrency, Base);
        }
    }
}
