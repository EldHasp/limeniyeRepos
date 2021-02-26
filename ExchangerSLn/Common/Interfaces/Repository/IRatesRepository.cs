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
        /// <param name="currencies">Список отслеживаемых валют.</param>
        void SetBaseCurrency(CurrencyDto @base, IEnumerable<CurrencyDto> currencies);
    }
}
