using DtoTypes;
using System.Collections.Generic;

namespace ExchangerModels
{
    public partial class ExchangerModel
    {
        

        /// <summary>Внутреннее поле "Только для чтения" со словарём валютныхх пар, которые взаимодействуют с внесённой валютой.</summary>
        private readonly Dictionary<int, ExchangeDto> exchanges;
        /// <summary>Неизменяемый словарь валютныхх пар, которые взаимодействуют с внесённой валютой..</summary>
        public IReadOnlyDictionary<int, ExchangeDto> Exchanges { get; }


        /// <summary>Внутреннее поле "Только для чтения" со словарём предложение по обмену базовой валюты.</summary>
        private readonly Dictionary<int, RateDto> rates;
        /// <summary>Неизменяемый словарь предложение по обмену базовой валюты.</summary>
        public IReadOnlyDictionary<int, RateDto> Rates { get; }



    }
}
