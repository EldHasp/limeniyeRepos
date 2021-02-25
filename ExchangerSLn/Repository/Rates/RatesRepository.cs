using Common.EventsArgs;
using Common.Interfaces.Repository;
using DtoTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using System.Xml.Linq;
using xNet;

namespace Repository.Rates
{

    /// <summary>Класс репозитория для работы с сайтом ......</summary>
    public partial class RatesRepository : IRatesRepository
    {
        

        /// <summary>Внутреня коллекция курсов.</summary>
        private readonly List<RateDto> rates = new List<RateDto>();
        /// <summary>Базовая валюта.</summary>
        private CurrencyDto baseCurrency;
        private readonly List<CurrencyDto> currencies = new List<CurrencyDto>();

        public Task<RateDto> GetCurrencyRateAsync(CurrencyDto currency, CurrencyDto @base)
        {
            throw new NotImplementedException();
        }

        public void SetBaseCurrency(CurrencyDto @base, IEnumerable<CurrencyDto> currencies)
        {
            rates.Clear();
            // Очистка коллекции
            ChangedRates?.Invoke(this, NotifyCollectionChangedEventArgs.Clear<RateDto>());
            //а ка коно понимает что нужно очистит <see rates

            baseCurrency = @base;

            SetCurrencies(currencies);
        }

        /// <summary>Метод задания прослушиваемых валют.</summary>
        /// <param name="rates">Список валют.</param>
        /// <remarks>В общем случае, здесь может быть подписка на какнал сервера.
        /// В нашем случае просто установка списка  <see cref="currencies"/> и запуск таймера.</remarks>
        private void SetCurrencies(IEnumerable<CurrencyDto> currencies)
        {
            timer.Stop();

            this.currencies.Clear();
            this.currencies.AddRange(currencies);

            timer.Elapsed -= RenderRates;
            timer.Elapsed += RenderRates;

            timer.Interval = 1000 * 10; //1000 * 60 * 30;

            timer.AutoReset = true;
        }

        /// <summary>Метод обновления курсов валют.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenderRates(object sender, ElapsedEventArgs e)
        {
            
            var resultList = GetAllRatesOfCurrencyAsync(baseCurrency, currencies).Result;
            foreach (var item in resultList)
            {
                //проверка на существование в списке такой пары, если нет -- создаётся новая
                if(rates.Find(x => x.Id == item.Id) == null)
                    ChangedRates.Invoke.
                if (!item.Equals(rates.Find(x => x.Id == item.Id)))
                    
            }
        }

        private readonly Timer timer = new Timer();

        /// <summary> Метод завращает пару валют с их курсом</summary>
        /// <remarks> Если <see cref="currency"/> будет USD, а <see cref="@base"/> будет UAH, то курс будет в UAH.</remarks>
        public async Task<RateDto> GetRateOfCurrencyAsync(CurrencyDto currency, CurrencyDto @base)
        {
            HttpRequest request = new HttpRequest();
            RequestParams rParams = new RequestParams();
            rParams["base"] = @base.Symbol;
            rParams["symbols"] = currency.Symbol;
            rParams["format"] = "XML";
            HttpResponse response = await Task.Run(() => request.Get("https://api.exchangerate.host/latest", rParams));
            string responseXmlResult = "";
            using (StreamReader sr = new StreamReader(response.ToMemoryStream()))
            {
                responseXmlResult = sr.ReadToEnd();
            }

            XDocument doc = XDocument.Parse(responseXmlResult);

            XElement element = doc.Descendants("data").First();
            decimal rate = Convert.ToDecimal(element.Descendants("rate").Select(v => (string)v).FirstOrDefault());
            RateDto result = new RateDto(@base, currency, rate);
            return result;
        }


        /// <summary> Метод получает список курсов заданных валют относительно базовой</summary>
        /// <param name="base"> Базовая валюта </param>
        /// <param name="available"> Список доступных валют.</param>
        /// <returns> Список валют с курсами </returns>
        public async Task<List<RateDto>> GetAllRatesOfCurrencyAsync(CurrencyDto @base, IList<CurrencyDto> available)
        {
            if (available == null)
                return null;

            HttpRequest request = new HttpRequest();
            RequestParams rParams = new RequestParams();
            rParams["base"] = @base.Symbol;
            string symbols = available.First().Symbol;
            for (int i = 1; i < available.Count; i++)
            {
                symbols += "," + available[i].Symbol;
            }
            rParams["symbols"] = symbols;
            rParams["format"] = "XML";
            HttpResponse response = await Task.Run(() => request.Get("https://api.exchangerate.host/latest", rParams));
            string responseXmlResult = "";
            using (StreamReader sr = new StreamReader(response.ToMemoryStream()))
            {
                responseXmlResult = sr.ReadToEnd();
            }


            XDocument doc = XDocument.Parse(responseXmlResult);
            var elements = doc.Descendants("data");

            List<RateDto> tempList = new List<RateDto>();
            foreach (var item in elements)
            {
                decimal rate = Convert.ToDecimal(item.Descendants("rate").Select(v => (string)v).FirstOrDefault());
                string currencyName = item.Descendants("code").Select(v => (string)v).FirstOrDefault();
                tempList.Add(new RateDto(available.First(x => x.Symbol == currencyName), @base, rate));
            }
            return tempList;
        }


        public RatesRepository()
        {

        }


    }
}
