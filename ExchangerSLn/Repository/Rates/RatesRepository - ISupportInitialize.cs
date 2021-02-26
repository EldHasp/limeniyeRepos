using Common;
using Common.Interfaces.Repository;
using DtoTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Repository.Rates
{
    public partial class RatesRepository : ISupportInitializeRatesRepository
    {
        void ISupportInitialize.BeginInit()
        {
            if (IsBeginInit)
                throw new MethodAccessException("Нельзя начинать новую инициализацию пока не закончена предыдущая транзакциия инициализации.");
            IsBeginInit = true;
            timer.Stop();
        }

        void ISupportInitialize.EndInit()
        {
            if (!IsBeginInit)
                throw new MethodAccessException("Транзакциия инициализации не была начата.");

            if (baseCurrency == null)
                baseCurrency = AllCurrencies.First();

            IsBeginInit = false;

            SetBaseCurrency(baseCurrency);

        }

        void ISupportInitializeRatesRepository.SetAllCurrencies(IEnumerable<CurrencyDto> allCurrencies)
        {
            if (!IsBeginInit)
                throw new MethodAccessException("Метод можно вызывать только во время транзакции инициализации.");
            AllCurrencies = allCurrencies?.Distinct().Where(crr => crr != null).ToList().AsReadOnly();
        }

        void ISupportInitializeRatesRepository.Initialize(IEnumerable<CurrencyDto> available, CurrencyDto baseCurrency)
        {
            ISupportInitializeRatesRepository initializeRates = this;

            initializeRates.BeginInit();
            initializeRates.SetAllCurrencies(available.Prepend(baseCurrency));
            this.baseCurrency = baseCurrency ?? AllCurrencies.First();
            initializeRates.EndInit();
        }

        public bool IsBeginInit { get; private set; }
        public IReadOnlyList<CurrencyDto> AllCurrencies { get; private set; }
    }

}
