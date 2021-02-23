using System.Collections.ObjectModel;
using System.Linq;
using Common.Interfaces.Model;
using Common.Interfaces.ViewModel;
using DtoTypes;
using Simplified;

namespace ExchangerViewModels
{
    public class MainExchengerViewModel : BaseInpc, IMainExchangerViewModel
    {
        private readonly IExchangerModel model;

        #region Private Fields
        private CurrencyDto _baseCurrency;
        private decimal _amounBase = 0;
        #endregion

        #region Public Properties
        /// <summary> Главная валюта, по которой ориентируются все остальные. </summary>
        public CurrencyDto BaseCurrency { get => _baseCurrency; private set => Set(ref _baseCurrency, value); }

        /// <summary> Внесённые средства. </summary>
        public decimal AmounBase { get => _amounBase; private set => Set(ref _amounBase, value); }

        /// <summary> Нужна для отображения следующего окна. </summary>
        public IExchangeViewModel SelectedExchange => throw new System.NotImplementedException();
        #endregion


        #region RelayCommand
        /*---------------------------------------------------------------------------------------------------------------------*/
        private RelayCommand<CurrencyDto> _setBaseCurrencyCommand;
        public RelayCommand<CurrencyDto> SetBaseCurrencyCommand => _setBaseCurrencyCommand
             ?? (_setBaseCurrencyCommand = new RelayCommand<CurrencyDto>(SetBaseCurrencyMethod));

        private void SetBaseCurrencyMethod(CurrencyDto baseCurrency)
        {
            Exchanges.Clear();
            BaseCurrency = baseCurrency;

            foreach (var currency in Currencies.Where(cr => cr != BaseCurrency))
            {
                Exchanges.Add((IExchangeViewModel)model.GetExchange(currency, BaseCurrency, AmounBase));
            }
        }
        /*---------------------------------------------------------------------------------------------------------------------*/
        #endregion

        #region Collection
        public ObservableCollection<IExchangeViewModel> Exchanges { get; } = new ObservableCollection<IExchangeViewModel>();
        public ObservableCollection<CurrencyDto> Currencies { get; } = new ObservableCollection<CurrencyDto>();
        #endregion


        public MainExchengerViewModel(IExchangerModel model)
        {
            this.model = model;
            foreach (var currency in model.GetCurrencies())
                Currencies.Add(currency);
        }

    }
}
