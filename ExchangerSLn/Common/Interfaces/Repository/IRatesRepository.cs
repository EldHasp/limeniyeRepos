using Common.EventsArgs;
using DtoTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Interfaces.Repository
{
    public interface IRatesRepository
    {
        /// <summary>Получить курс пары.</summary>
        /// <param name="base"> Курс будет отображаться в этой валюте.</param>
        Task<RateDto> GetCurrencyRateAsync(CurrencyDto currency, CurrencyDto @base); // получение курса пары валют


        ///// <summary>Список пар относительно доступных.</summary>
        //Task<List<RateDto>> GetAllCurrencyRateAsync(CurrencyDto @base);


       /// <summary>Событие после изменения отслеживаемых курсов.</summary>
        event EventHandler<NotifyCollectionChangedEventArgs<RateDto>> ChangedRates;

        /// <summary>Задание базовой валюты.</summary>
        /// <param name="base">Базовая валюта.</param>
        /// <param name="currencies">Список отслеживаемых валют.</param>
        void SetBaseCurrency(CurrencyDto @base, IEnumerable<CurrencyDto> currencies);
    }
}
