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
        Task<IList<RateDto>> GetAllRatesOfCurrencyAsync();

        /// <summary> Метод получает список курсов заданных валют относительно базовой.</summary>
        /// <returns> Список валют с курсами.</returns>
        IList<RateDto> GetAllRatesOfCurrency();

    }

}
