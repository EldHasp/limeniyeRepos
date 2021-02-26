using DtoTypes;
using System.Collections.Generic;
using System.ComponentModel;

namespace Common.Interfaces.Repository
{
    public interface ISupportInitializeRatesRepository : ISupportInitialize
    {
        bool IsBeginInit { get; }
        IReadOnlyList<CurrencyDto> AllCurrencies { get; }
        void SetAllCurrencies(IEnumerable<CurrencyDto> allCurrencies);

        void Initialize(IEnumerable<CurrencyDto> available, CurrencyDto baseCurrency);

    }

}
