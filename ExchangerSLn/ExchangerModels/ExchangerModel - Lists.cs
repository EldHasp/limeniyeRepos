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

        private List<RateDto> rates;
        public IReadOnlyList<RateDto> Rates { get; }
    }
}
