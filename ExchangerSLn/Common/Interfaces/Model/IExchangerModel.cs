using Common.EventsArgs;
using DtoTypes;
using System.Collections.Generic;

namespace Common.Interfaces.Model
{
    /// <summary>Интерфейс модели обменика.</summary>
    public interface IExchangerModel
    {
        IReadOnlyCollection<ExchangeDto> Exchanges { get; }
        event ExchangesCnahgedHandler ExchangesCnahged;
        decimal CurrentSumm { get; }
    }
}
