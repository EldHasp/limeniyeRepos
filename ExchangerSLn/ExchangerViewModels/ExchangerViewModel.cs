using DtoTypes;
using ExchangerModels.Interface;
using ExchangerViewModels.Interfaces;
using Simplified;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExchangerViewModels
{
    public class ExchangerViewModel : BaseInpc, IExchangerViewModel
    {
        private readonly IExchangerModel model;

        #region Private Fields
        private CurrencyDto _baseCurrency;
        private decimal _amounBase;
        #endregion

        #region Public Properties
        public CurrencyDto BaseCurrency { get => _baseCurrency; private set => Set(ref _baseCurrency, value); }
        public decimal AmounBase { get => _amounBase; private set => Set(ref _amounBase, value); }
        #endregion

        #region RelayCommand
        /*---------------------------------------------------------------------------------------------------------------------*/
        private RelayCommand<(CurrencyDto BaseCurrency, decimal Amoun)> _setBaseCurrencyCommand;
        public RelayCommand<(CurrencyDto BaseCurrency, decimal Amoun)> SetBaseCurrencyCommand => _setBaseCurrencyCommand
             ?? (_setBaseCurrencyCommand = new RelayCommand<(CurrencyDto, decimal)>(SetBaseCurrencyMethod));

        private void SetBaseCurrencyMethod((CurrencyDto BaseCurrency, decimal Amoun) parameter)
        {
            Exchanges.Clear();
            BaseCurrency = parameter.BaseCurrency;
            AmounBase = parameter.Amoun;

            foreach (var currency in Currencies.Where(cr => cr != BaseCurrency))
            {
                Exchanges.Add(model.GetExchange(currency, BaseCurrency, AmounBase));
            }
        }

        /*---------------------------------------------------------------------------------------------------------------------*/
        #endregion

        #region Collection
        public ObservableCollection<ExchangeDto> Exchanges { get; } = new ObservableCollection<ExchangeDto>();
        public ObservableCollection<CurrencyDto> Currencies { get; } = new ObservableCollection<CurrencyDto>();
        #endregion


        public ExchangerViewModel(IExchangerModel model)
        {
            this.model = model;
            foreach (var currency in model.GetCurrencies())
                Currencies.Add(currency);
        }
    }
}
