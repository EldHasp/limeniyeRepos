using DtoTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangerModels.API
{
    public interface ICurrencyRateAPI
    {
        /// <summary>Получить курс пары.</summary>
        /// <param name="base"> Курс будет отображаться в этой валюте.</param>
        Task<RateDto> GetCurrencyRate(CurrencyDto currency, CurrencyDto @base);

        /// <summary>Список пар относительно доступных.</summary>
        Task<IList<RateDto>> GetAllCurrencyRate(CurrencyDto @base, IList<CurrencyDto> available);
    }
}
