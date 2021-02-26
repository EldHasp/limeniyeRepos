using DtoTypes;
using System.Collections.Generic;

namespace Repository.Rates
{
    public partial class RatesRepository
    {
        /// <summary>Внутреня список валют.</summary>
        private readonly List<CurrencyDto> currencies = new List<CurrencyDto>();
        /// <summary>Внутрий список курсов.</summary>
        private readonly List<RateDto> rates = new List<RateDto>();
    }
}
