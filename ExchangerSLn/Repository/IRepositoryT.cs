using DtoTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>Получить курс пары.</summary>
        /// <param name="base"> Курс будет отображаться в этой валюте.</param>
        Task<T>GetCurrencyRate(CurrencyDto currency, CurrencyDto @base); // получение курса пары валют
        /// <summary>Список пар относительно доступных.</summary>
        Task<IList<T>> GetAllCurrencyRate(CurrencyDto @base, IList<CurrencyDto> available);
    }
}
