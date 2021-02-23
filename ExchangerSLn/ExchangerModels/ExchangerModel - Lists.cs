using DtoTypes;
using System.Collections.Generic;

namespace ExchangerModels
{
    public partial class ExchangerModel
    {
        /// <summary>Неизменяемый словарь предложение по обмену базовой валюты.</summary>
        public IReadOnlyDictionary<RateDto, ExchangeDto> Exchanges { get; }

        /// <summary>Внутреннее поле "Только для чтения" со словарём предложение по обмену базовой валюты.</summary>
        private readonly Dictionary<RateDto, ExchangeDto> exchanges;

        /// <summary>Неизменяемый словарь доступных, для пар, валют.</summary>
        private IReadOnlyList<CurrencyDto> availableCurrency { get; } = new List<CurrencyDto>
        {
            new CurrencyDto("USD", "$"),
            new CurrencyDto("EUR", "€"),
            new CurrencyDto("RUB", "₽"),
            new CurrencyDto("CNY", "¥"),
            new CurrencyDto("KZT", "₸"),
            new CurrencyDto("VND", "₫"),
            new CurrencyDto("CHF", "₣")
        };
    }
}
