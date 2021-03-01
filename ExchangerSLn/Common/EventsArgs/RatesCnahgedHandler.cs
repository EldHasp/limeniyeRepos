using Common.Enums;
using DtoTypes;
using System.Collections.Generic;

namespace Common.EventsArgs
{
    public delegate void RatesCnahgedHandler(object sender, RatesAction action, IEnumerable<RateDto> newRates);
    public delegate void ExchangesCnahgedHandler(object sender, RatesAction action, IEnumerable<ExchangeDto> newExchanges);
}
