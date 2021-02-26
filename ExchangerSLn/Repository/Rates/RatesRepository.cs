using Common;
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


        public void SetBaseCurrency(CurrencyDto @base)
        {
            timer.Stop();
            // Очистка коллекции
            ClearRates();
            baseCurrency = @base;
            if (IsBeginInit)
                return;

            currencies.Clear();
            currencies.AddRange(AllCurrencies.Where(crr => crr != baseCurrency));

            RenderRates();
        }

        private static readonly Random rand = new Random();

        /// <summary>Метод обновления курсов валют.</summary>
        private void RenderRates(object sender = null, ElapsedEventArgs e = null)
        {
            timer.Stop();
            IList<RateDto> resultList = GetAllRatesOfCurrency();

            // TODO : следующие строки до комментария черты добавляют рандомные значения к валютам только для демонстрации!
            // TODO : После теста их нужно будет удалить.

            var rands = Enumerable.Range(0, resultList.Count)
                .OrderBy(x => rand.Next())
                .ToArray();

            int rand1 = rands[0];
            int rand2 = rands[1];
            int rand3 = rands[2];

            if (rands.Distinct().Count() != 3)
                Console.WriteLine("Совпадение");

            int valueRandom1 = rand.Next(1, 5);
            int valueRandom2 = rand.Next(1, 7);
            int valueRandom3 = rand.Next(1, 6);
            resultList[rand1] = new RateDto(resultList[rand1].Currency, resultList[rand1].Base, resultList[rand1].Rate * valueRandom1);
            resultList[rand2] = new RateDto(resultList[rand2].Currency, resultList[rand2].Base, resultList[rand2].Rate * valueRandom2);
            resultList[rand3] = new RateDto(resultList[rand3].Currency, resultList[rand3].Base, resultList[rand3].Rate * valueRandom3);

            //-----------------------------------------------

            // Удаление не изменившихся курсов
            resultList.RemoveAll(rt => rates.FirstOrDefault(rate => rate.Base == rt.Base && rate.Currency == rt.Currency)?.Rate == rt.Rate);

            AddRangeRates(resultList);
            timer.Start();
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
        /// <param name="currencies"> Список доступных валют.</param>
        /// <returns> Список валют с курсами </returns>
        private async Task<IList<RateDto>> GetAllRatesOfCurrencyAsync()
        {
            return await Task.Run(GetAllRatesOfCurrency);
        }

        private static readonly RateDto[] emptyRates = new RateDto[0];

        /// <summary> Метод получает список курсов заданных валют относительно базовой</summary>
        /// <param name="baseCurrency"> Базовая валюта </param>
        /// <returns> Список валют с курсами </returns>
        private IList<RateDto> GetAllRatesOfCurrency()
        {
            if (baseCurrency == null || currencies == null || !currencies.Any())
                return emptyRates;

            string symbols = string.Join(",", currencies.Select(crr => crr.Symbol));

            HttpRequest request = new HttpRequest();
            RequestParams rParams = new RequestParams();
            rParams["base"] = baseCurrency.Symbol;
            rParams["symbols"] = symbols;
            rParams["format"] = "XML";
            HttpResponse response = request.Get("https://api.exchangerate.host/latest", rParams);
            string responseXmlResult = "";
            using (StreamReader sr = new StreamReader(response.ToMemoryStream()))
            {
                responseXmlResult = sr.ReadToEnd();
            }


            XDocument doc = XDocument.Parse(responseXmlResult);
            var elements = doc.Root.Elements("data");

            List<RateDto> tempList = new List<RateDto>();
            foreach (var item in elements)
            {
                decimal rate = Convert.ToDecimal(item.Element("rate").Value) * commission;
                CurrencyDto currency = AllCurrencies.FirstOrDefault(crr => crr.Symbol == item.Element("code").Value);
                tempList.Add(new RateDto(currency, baseCurrency, rate));
            }
            return tempList;
        }
        #endregion


        #region Для пользователя
        public IEnumerable<RateDto> GetAllRates()
        {
            return rates.GetEnumerable();
        }
        public Task<RateDto> GetRatesAsync(CurrencyDto currency)
        {
            var task = rates.Find(x => x.Base == baseCurrency && x.Currency == currency);
            return Task.Run(() => task);
        }
        #endregion


    }
}
