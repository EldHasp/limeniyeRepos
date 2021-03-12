using Common;
using Common.Interfaces.Repository;
using DtoTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using xNet;

namespace Repository.Rates
{
    /// <summary>Класс репозитория для работы с сайтом currencyconverterapi.com </summary>
    public partial class RatesRepository : IRatesRepository
    {
        private CurrencyDto baseCurrency; 
        private readonly string api = "cfc41fa0a34e8e1f4bf2";
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
            currencies.AddRange(AllCurrencies.Where(crr => crr.Symbol != baseCurrency.Symbol));

            RenderRates();
        }

        public CurrencyDto GetBaseCurrency()
        {
            return baseCurrency;
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
                IList<RateDto> resultList = GetAllRatesOfCurrencyTest();

                // TODO : следующие строки до комментария черты добавляют рандомные значения к валютам только для демонстрации!
                // TODO : После теста их нужно будет удалить.
                //#region Строки для теста
                //var rands = Enumerable.Range(0, resultList.Count)
                //    .OrderBy(x => rand.Next())
                //    .ToArray();

                //int rand1 = rands[0];
                //int rand2 = rands[1];
                //int rand3 = rands[2];

                //if (new int[] { rand1, rand2, rand3 }.Distinct().Count() != 3)
                //    Console.WriteLine("Совпадение");

                //int valueRandom1 = rand.Next(2, 5);
                //int valueRandom2 = rand.Next(2, 7);
                //int valueRandom3 = rand.Next(2, 6);
                //resultList[rand1] = new RateDto(resultList[rand1].Currency, resultList[rand1].Base, resultList[rand1].Rate * valueRandom1);
                //resultList[rand2] = new RateDto(resultList[rand2].Currency, resultList[rand2].Base, resultList[rand2].Rate * valueRandom2);
                //resultList[rand3] = new RateDto(resultList[rand3].Currency, resultList[rand3].Base, resultList[rand3].Rate * valueRandom3);
                //#endregion
                ////-----------------------------------------------

                // Удаление не изменившихся курсов
                resultList.RemoveAll(rt => rates.FirstOrDefault(rate => rate.Base == rt.Base && rate.Currency == rt.Currency)?.Rate == rt.Rate);

                AddRangeRates(resultList);
            //}
            //catch { }

            timer.Start();
        }




        private IList<RateDto> GetAllRatesOfCurrencyTest()
        {
            if (baseCurrency == null || currencies == null || !currencies.Any())
                return emptyRates;

            string symbols = string.Join(",", currencies.Select(crr => crr.Symbol + "_" + baseCurrency.Symbol));
            List<RateDto> tempList = new List<RateDto>();
            #region UAH
            if (baseCurrency.Symbol == "UAH")
            {
                tempList.Add(new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.83579m));
                tempList.Add(new RateDto(new CurrencyDto("EUR", "€"), new CurrencyDto("UAH", "₴"), 33.128183m));
                tempList.Add(new RateDto(new CurrencyDto("RUB", "₽"), new CurrencyDto("UAH", "₴"), 0.376518m));
                tempList.Add(new RateDto(new CurrencyDto("CNY", "¥"), new CurrencyDto("UAH", "₴"), 4.272765m));
                tempList.Add(new RateDto(new CurrencyDto("KZT", "₸"), new CurrencyDto("UAH", "₴"), 0.066116m));
                tempList.Add(new RateDto(new CurrencyDto("VND", "₫"), new CurrencyDto("UAH", "₴"), 0.001205m));
                tempList.Add(new RateDto(new CurrencyDto("CHF", "₣"), new CurrencyDto("UAH", "₴"), 29.890777m));
            }
            #endregion

            #region USD
            if (baseCurrency.Symbol == "USD")
            {
                tempList.Add(new RateDto(new CurrencyDto("UAH", "₴"), new CurrencyDto("USD", "$"), 0.035925m));
                tempList.Add(new RateDto(new CurrencyDto("EUR", "€"), new CurrencyDto("USD", "$"), 1.190129m));
                tempList.Add(new RateDto(new CurrencyDto("RUB", "₽"), new CurrencyDto("USD", "$"), 0.013526m));
                tempList.Add(new RateDto(new CurrencyDto("CNY", "¥"), new CurrencyDto("USD", "$"), 0.153499m));
                tempList.Add(new RateDto(new CurrencyDto("KZT", "₸"), new CurrencyDto("USD", "$"), 0.002375m));
                tempList.Add(new RateDto(new CurrencyDto("VND", "₫"), new CurrencyDto("USD", "$"), 0.000043306508m));
                tempList.Add(new RateDto(new CurrencyDto("CHF", "₣"), new CurrencyDto("USD", "$"), 1.073825m));
            }
            #endregion

            #region EUR
            if (baseCurrency.Symbol == "EUR")
            {
                tempList.Add(new RateDto(new CurrencyDto("UAH", "₴"), new CurrencyDto("EUR", "€"), 0.030178m));
                tempList.Add(new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("EUR", "€"), 0.840025m));
                tempList.Add(new RateDto(new CurrencyDto("RUB", "₽"), new CurrencyDto("EUR", "€"), 0.011363m));
                tempList.Add(new RateDto(new CurrencyDto("CNY", "¥"), new CurrencyDto("EUR", "€"), 0.128997m));
                tempList.Add(new RateDto(new CurrencyDto("KZT", "₸"), new CurrencyDto("EUR", "€"), 0.001996m));
                tempList.Add(new RateDto(new CurrencyDto("VND", "₫"), new CurrencyDto("EUR", "€"), 0.000036378549m));
                tempList.Add(new RateDto(new CurrencyDto("CHF", "₣"), new CurrencyDto("EUR", "€"), 0.902416m));
            }
            #endregion

            #region RUB
            if (baseCurrency.Symbol == "RUB")
            {
                tempList.Add(new RateDto(new CurrencyDto("UAH", "₴"), new CurrencyDto("RUB", "₽"), 2.655161m));
                tempList.Add(new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("RUB", "₽"), 73.908495m));
                tempList.Add(new RateDto(new CurrencyDto("EUR", "€"), new CurrencyDto("RUB", "₽"), 88.049913m));
                tempList.Add(new RateDto(new CurrencyDto("CNY", "¥"), new CurrencyDto("RUB", "₽"), 11.359237m));
                tempList.Add(new RateDto(new CurrencyDto("KZT", "₸"), new CurrencyDto("RUB", "₽"), 0.175689m));
                tempList.Add(new RateDto(new CurrencyDto("VND", "₫"), new CurrencyDto("RUB", "₽"), 0.003201m));
                tempList.Add(new RateDto(new CurrencyDto("CHF", "₣"), new CurrencyDto("RUB", "₽"), 79.465266m));
            }
            #endregion

            return tempList;
        }


    }
}
