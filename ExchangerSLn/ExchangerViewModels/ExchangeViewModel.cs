using System.Collections.ObjectModel;
using Common.Interfaces.Model;
using Common.Interfaces.ViewModel;
using DtoTypes;
using Simplified;

namespace ExchangerViewModels
{
    public class ExchangerViewModel : BaseInpc, IExchegerViewModel
    {
        private readonly IExchangerModel model;

        #region Private Fields
        private CurrencyDto _baseCurrency;
        private decimal _amounBase = 0;
        private ExchangeDto _selectedExchange;
        #endregion

        #region Public Properties
        /// <summary> Главная валюта, по которой ориентируются все остальные. </summary>
        public CurrencyDto BaseCurrency { get => _baseCurrency; private set => Set(ref _baseCurrency, value); }

        /// <summary> Внесённые средства. </summary>
        public decimal AmounBase { get => _amounBase; private set => Set(ref _amounBase, value); }

        /// <summary> Выбор валюты </summary>
        public ExchangeDto SelectedExchange { get => _selectedExchange; private set => Set(ref _selectedExchange, value); }
        #endregion

        /// <summary> Список предложений по обмену </summary>
        public ObservableCollection<ExchangeDto> Exchanges { get; } = new ObservableCollection<ExchangeDto>();

        public ExchangerViewModel(IExchangerModel model)
        {
            this.model = model;
        }
    }
}
