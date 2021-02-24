using Common.EventsArgs;
using Common.Interfaces.Repository;
using DtoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Repository.Rates
{

    /// <summary>Класс репозитория для работы с сайтом ......</summary>
    public class RatesRepository : IRatesRepository
    {
        public event EventHandler<NotifyCollectionChangedEventArgs<RateDto>> ChangedRates;

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

            // Раз в 30 минут?
            timer.Interval = 1000 * 60 * 30;
            
            timer.
        }

        /// <summary>Метод обновления курсов валют.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenderRates(object sender, ElapsedEventArgs e)
        {
            // Здесь запрос курсов, сверка с коллекцией Rates.
            // Создание события ChangedRates
        }

        private readonly Timer timer = new Timer();

    }
}
