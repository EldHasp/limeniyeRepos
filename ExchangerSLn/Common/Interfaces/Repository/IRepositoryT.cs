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
        Task GetCurrencyRate(CurrencyDto currency, CurrencyDto @base); // получение курса пары валют
        /// <summary>Список пар относительно доступных.</summary>
        Task GetAllCurrencyRate(CurrencyDto @base);
    }
}
