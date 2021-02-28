using Common;
using Common.Interfaces.Repository;
using DtoTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using xNet;

namespace Repository.Rates
{
    /// <summary>Класс репозитория для работы с сайтом currencyconverterapi.com </summary>
    public partial class RatesRepository : IRatesRepository
    {
        private CurrencyDto baseCurrency;
        private readonly string api = "6db2795decb1d7bad58e";
        private readonly string type = "free";
        private readonly Timer timer = new Timer();
        private static readonly Random rand = new Random();
        private static readonly RateDto[] emptyRates = new RateDto[0];

        #region Public Methods
        public void SetBaseCurrency(CurrencyDto @base)
        {
            timer.Stop();
            ClearRates();
            baseCurrency = @base;
            if (IsBeginInit)
                return;

            currencies.Clear();
            currencies.AddRange(AllCurrencies.Where(crr => crr != baseCurrency));

            RenderRates();
        }
        
        public IEnumerable<RateDto> GetCurrentRates()
        {
            return rates.GetEnumerable();
        }

        public Task<RateDto> GetRatesAsync(CurrencyDto currency)
        {
            var task = rates.Find(x => x.Base == baseCurrency && x.Currency == currency);
            return Task.Run(() => task);
        }
        #endregion

        #region Requests
        /// <summary> Метод завращает пару валют с их курсом</summary>
        /// <remarks> Если <see cref="currency"/> будет USD, а <see cref="@base"/> будет UAH, то курс будет в UAH.</remarks>
        public RateDto GetRateOfCurrencyAsync(CurrencyDto currency, CurrencyDto @base)
        {
            HttpRequest request = new HttpRequest();
            RequestParams rParams = new RequestParams();
            rParams["base"] = @base.Symbol;
            rParams["symbols"] = currency.Symbol;
            rParams["format"] = "json";
            HttpResponse response = request.Get("https://"+ type + ".exchangerate.host/latest", rParams);
            string responseXmlResult = "";
            using (StreamReader sr = new StreamReader(response.ToMemoryStream()))
            {
                responseXmlResult = sr.ReadToEnd();
            }

            RatesJson ratesJson = JsonSerializer.Deserialize<RatesJson>(responseXmlResult);
            RateDto tempList = null;
              //  (RateDto)ratesJson.rates.Select(rt => new RateDto(AllCurrencies.FirstOrDefault(crr => crr.Symbol == rt.Key),baseCurrency,rt.Value));

            return tempList;
        }

        /// <summary> Метод получает список курсов заданных валют относительно базовой</summary>
        /// <returns> Список валют с курсами </returns>
        private IList<RateDto> GetAllRatesOfCurrency()
        {
            if (baseCurrency == null || currencies == null || !currencies.Any())
                return emptyRates;

            string symbols = string.Join(",", currencies.Select(crr => crr.Symbol + "_" + baseCurrency.Symbol));

            HttpRequest request = new HttpRequest();
            RequestParams rParams = new RequestParams();
            rParams["q"] = symbols;
            rParams["apiKey"] = api;
            HttpResponse response = request.Get("https://" + type + ".currconv.com/api/v7/Convert", rParams);


            string responseJsonResult = response.ToString();
            
            var data = JObject.Parse(responseJsonResult)["results"].ToArray();
            var test = data.Select(item => item.First.ToObject<CurrencyResponse>()).ToList();
            
            List<RateDto> tempList = test.Select(rt => new RateDto(AllCurrencies.FirstOrDefault(crr => crr.Symbol == rt.Fr), baseCurrency,rt.Val)).ToList();
            return tempList;
        }
        #endregion

        /// <summary>Метод обновления курсов валют.</summary>
        private void RenderRates(object sender = null, ElapsedEventArgs e = null)
        {
            timer.Stop();
            //try
            //{
                IList<RateDto> resultList = GetAllRatesOfCurrency();

                // TODO : следующие строки до комментария черты добавляют рандомные значения к валютам только для демонстрации!
                // TODO : После теста их нужно будет удалить.
                #region Строки для теста
                var rands = Enumerable.Range(0, resultList.Count)
                    .OrderBy(x => rand.Next())
                    .ToArray();

                int rand1 = rands[0];
                int rand2 = rands[1];
                int rand3 = rands[2];

                if (new int[] { rand1, rand2, rand3 }.Distinct().Count() != 3)
                    Console.WriteLine("Совпадение");

                int valueRandom1 = rand.Next(2, 5);
                int valueRandom2 = rand.Next(2, 7);
                int valueRandom3 = rand.Next(2, 6);
                resultList[rand1] = new RateDto(resultList[rand1].Currency, resultList[rand1].Base, resultList[rand1].Rate * valueRandom1);
                resultList[rand2] = new RateDto(resultList[rand2].Currency, resultList[rand2].Base, resultList[rand2].Rate * valueRandom2);
                resultList[rand3] = new RateDto(resultList[rand3].Currency, resultList[rand3].Base, resultList[rand3].Rate * valueRandom3);
                #endregion
                //-----------------------------------------------

                // Удаление не изменившихся курсов
                resultList.RemoveAll(rt => rates.FirstOrDefault(rate => rate.Base == rt.Base && rate.Currency == rt.Currency)?.Rate == rt.Rate);

                AddRangeRates(resultList);
            //}
            //catch { }

            timer.Start();
        }
    }
}
