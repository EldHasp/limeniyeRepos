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
        }

        void ISupportInitialize.EndInit()
        {
            if (!IsBeginInit)
                throw new MethodAccessException("Транзакциия инициализации не была начата.");

            if (baseCurrency != null)
            {
                // Запрос курсов по новому списку AllCurrencies

                SetBaseCurrency(baseCurrency);
            }
            else
            {
                // Сброс курсов
                ClearRates();
            }
        }

        void ISupportInitializeRatesRepository.SetAllCurrencies(IEnumerable<CurrencyDto> allCurrencies)
        {
            if (!IsBeginInit)
                throw new MethodAccessException("Метод можно вызывать только во время транзакции инициализации.");
            AllCurrencies = allCurrencies.ToList().AsReadOnly();
        }

        public bool IsBeginInit { get; private set; }
        public IReadOnlyList<CurrencyDto> AllCurrencies { get; private set; }
    }

}
