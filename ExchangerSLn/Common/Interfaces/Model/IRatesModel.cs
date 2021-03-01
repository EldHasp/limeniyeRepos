using Common.EventsArgs;
using DtoTypes;
using System.Collections.Generic;

namespace Common.Interfaces.Model
{
    /// <summary>Интерфейс модели справочника курсов.</summary>
    public interface IRatesModel : IBaseCurrencyModel
    {
        /// <summary>Коллекция курсов по отношению к <see cref="BaseCurrency"/>.</summary>
        IReadOnlyList<RateDto> Rates { get; }

        /// <summary>Событие извещающее об изменении <see cref="Rates"/>.</summary>
        event RatesCnahgedHandler RatesCnahged;


    }

}
