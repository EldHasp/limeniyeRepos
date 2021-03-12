using Common.EventsArgs;
using Common.Interfaces.Repository;
using DtoTypes;
using System.Collections.Generic;

namespace Repository.Rates
{

    /// <summary>Класс репозитория для работы с сайтом ......</summary>
    public partial class RatesRepository : IRatesRepository
    {
        /// <summary>Создаёт экземпляр репозитория.</summary>
        /// <param name="commission">Комиссия за обмен.</param>
        /// <param name="available">Список отслеживаемых валют.</param>
        /// <param name="baseCurrency">Базовая валюта.</param>
        /// <param name="onRatesCnahged">Прослушка события <see cref="RatesCnahged"/>.</param>
        public RatesRepository(decimal commission, IEnumerable<CurrencyDto> available, RatesCnahgedHandler onRatesCnahged, CurrencyDto baseCurrency = null)
            : this(commission)
        {
            ((ISupportInitializeRatesRepository)this).Initialize(available, baseCurrency);
            RatesCnahged += onRatesCnahged;
        }
        /// <summary>Создаёт экземпляр репозитория.</summary>
        /// <param name="commission">Комиссия за обмен.</param>
        /// <param name="available">Список отслеживаемых валют.</param>
        /// <param name="baseCurrency">Базовая валюта.</param>
        public RatesRepository(decimal commission, IEnumerable<CurrencyDto> available, CurrencyDto baseCurrency = null)
            : this(commission)
        {
            ((ISupportInitializeRatesRepository)this).Initialize(available, baseCurrency);
        }

        /// <summary>Комиссия за обмен.</summary>
        private readonly decimal commission;

        /// <summary>Создаёт экземпляр репозитория.</summary>
        /// <param name="commission">Комиссия за обмен. 
        /// Задаётся как увеличивающий коэфициент.
        /// Например, комиссия 2% задаётся как 1,02.</param>
        public RatesRepository(decimal commission)
        {
            this.commission = commission;

            timer.Elapsed += RenderRates;
            timer.AutoReset = true;
            timer.Interval = 600000;
        }
    }
}
