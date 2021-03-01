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
        private readonly IMainModel  model;
        //private readonly IRatesRepository repositoryModel;
        #endregion

        #region ViewModels
        public IRatesViewModel RatesVM { get; }
        public IExchegerViewModel ExchegerVM { get; }
        #endregion


        public MainViewModel( IMainModel model)
        {
            this.model = model;
            //this.repositoryModel = repositoryModel;

            RatesVM = new RatesViewModel(model);
            ExchegerVM = new ExchangerViewModel(model);
        }       
    }
}
