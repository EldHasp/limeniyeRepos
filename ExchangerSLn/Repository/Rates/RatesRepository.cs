using Common.Interfaces.Repository;
using DtoTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using xNet;

namespace Repository.Rates
{

    /// <summary>Класс репозитория для работы с сайтом ......</summary>
    public partial class RatesRepository : IRatesRepository
    {
        /// <summary>Базовая валюта.</summary>
        private CurrencyDto baseCurrency;
        private readonly Timer timer = new Timer();


        public void SetBaseCurrency(CurrencyDto @base, IEnumerable<CurrencyDto> currencies)
        {
            rates.Clear();
            // Очистка коллекции
            ClearRates();
            baseCurrency = @base;

            SetCurrencies(currencies);
        }

        /// <summary>Метод задания прослушиваемых валют.</summary>
        /// <param name="rates">Список валют.</param>
        /// <remarks>В общем случае, здесь может быть подписка на канал сервера.
        /// В этом случае просто установка списка  <see cref="currencies"/> и запуск таймера.</remarks>
        private void SetCurrencies(IEnumerable<CurrencyDto> currencies)
        {
            timer.Stop();

            this.currencies.Clear();
            this.currencies.AddRange(currencies);

            timer.Elapsed -= RenderRates;
            timer.Elapsed += RenderRates;

            timer.AutoReset = true;
            timer.Enabled = true;
            
        }

        /// <summary>Метод обновления курсов валют.</summary>
        private void RenderRates(object sender, ElapsedEventArgs e)
        {
            timer.Interval = 1000 * 20; //1000 * 60 * 30;
            var resultList = GetAllRatesOfCurrencyAsync(baseCurrency, currencies).Result;

            // TODO : следующие строки до комментария черты добавляют рандомные значения к валютам только для демонстрации!
            // TODO : После теста их нужно будет удалить.
            Random rand = new Random(); int rand1 = rand.Next(0, resultList.Count); int rand2 = rand.Next(0, resultList.Count); int rand3 = rand.Next(0, resultList.Count);
            int valueRandom1 = rand.Next(1, 5); int valueRandom2 = rand.Next(1, 7); int valueRandom3 = rand.Next(1, 6);
            resultList[rand1] = new RateDto(resultList[rand1].Currency, resultList[rand1].Base, resultList[rand1].Rate * valueRandom1);
            resultList[rand2] = new RateDto(resultList[rand2].Currency, resultList[rand2].Base, resultList[rand2].Rate * valueRandom2);
            resultList[rand3] = new RateDto(resultList[rand3].Currency, resultList[rand3].Base, resultList[rand3].Rate * valueRandom3);

            //-----------------------------------------------
            AddRangeRates(resultList);
        }

        #region Запросы
        /// <summary> Метод завращает пару валют с их курсом</summary>
        /// <remarks> Если <see cref="currency"/> будет USD, а <see cref="@base"/> будет UAH, то курс будет в UAH.</remarks>
        private async Task<RateDto> GetRateOfCurrencyAsync(CurrencyDto currency, CurrencyDto @base)
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
        private async Task<List<RateDto>> GetAllRatesOfCurrencyAsync(CurrencyDto @base, IList<CurrencyDto> available)
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
        #endregion


        #region Для пользователя
        public IEnumerable<RateDto> GetAllRates()
        {
            return rates;
        }
        public Task<RateDto> GetRates(CurrencyDto currency)
        {
            var task = rates.Find(x => x.Base == baseCurrency && x.Currency == currency);
            return Task.Run(() => task);
        }
        #endregion


        public RatesRepository(CurrencyDto baseCurrency, IList<CurrencyDto> available)
        {
            rates = GetAllRatesOfCurrencyAsync(baseCurrency, available).Result;
            SetBaseCurrency(baseCurrency, available);

        }
    }
}
