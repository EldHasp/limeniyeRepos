using Common;
using Common.EventsArgs;
using Common.Interfaces.Repository;
using DtoTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using xNet;

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
            RatesCnahged += onRatesCnahged;

            ((ISupportInitializeRatesRepository)this).Initialize(available, baseCurrency);
        }
        /// <summary>Создаёт экземпляр репозитория.</summary>
        /// <param name="commission">Комиссия за обмен.</param>
        /// <param name="available">Список отслеживаемых валют.</param>
        /// <param name="baseCurrency">Базовая валюта.</param>
        public RatesRepository(decimal commission, IEnumerable<CurrencyDto> available, CurrencyDto baseCurrency = null)
            : this(commission)
        {
            //SetBaseCurrency(baseCurrency, available);

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
            timer.Interval = 5000;
            //timer.Enabled = true;
        }
    }
}
