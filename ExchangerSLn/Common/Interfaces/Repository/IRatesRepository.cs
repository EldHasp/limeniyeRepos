using Common.EventsArgs;
using DtoTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Interfaces.Repository
{
    public interface IRatesRepository
    {
        /// <summary>Событие после изменения отслеживаемых курсов.</summary>
        event RatesCnahgedHandler RatesCnahged;

        /// <summary>Задание базовой валюты.</summary>
        /// <param name="base">Базовая валюта.</param>
        void SetBaseCurrency(CurrencyDto @base);

        /// <summary> Метод получает список курсов заданных валют относительно базовой.</summary>
        /// <returns> Список валют с курсами.</returns>
        IEnumerable<RateDto> GetCurrentRates();

        /// <summary>Асинхронный метод получения курса валюты по отношению к базовой.</summary>
        /// <param name="currency">Оцениваемая валюта.</param>
        /// <returns>Курс валюты.</returns>
        Task<RateDto> GetRatesAsync(CurrencyDto currency);

        /// <summary> Первичное получение текущей валюты </summary>
        CurrencyDto GetBaseCurrency();
    }

}
