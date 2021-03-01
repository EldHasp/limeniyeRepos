using System.Collections.ObjectModel;
using System.Linq;
using Common.Interfaces.Model;
using Common.Interfaces.Repository;
using Common.Interfaces.ViewModel;
using DtoTypes;
using Simplified;
using ViewModel.Currency;

namespace ExchangerViewModels
{
    public class MainViewModel : BaseInpc, IMainExchangerViewModel
    {
        #region Models
        private readonly IExchangerModel exchangerModel;
        private readonly IRatesRepository repositoryModel;
        #endregion

        #region ViewModels
        public IRatesViewModel RatesVM { get; }
        public IExchegerViewModel ExchegerVM { get; }
        #endregion


        public MainViewModel( IRatesRepository repositoryModel, IExchangerModel exchangerModel )
        {
            this.exchangerModel = exchangerModel;
            this.repositoryModel = repositoryModel;

            RatesVM = new RatesViewModel(this.repositoryModel);
            ExchegerVM = new ExchangerViewModel(this.exchangerModel);
        }       
    }
}
