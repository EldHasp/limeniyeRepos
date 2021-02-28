using Common.EventsArgs;
using System.Collections.ObjectModel;
using Simplified;
using DtoTypes;
using Common.Interfaces.Repository;
using Common;
using System.Linq;

namespace ViewModel.Currency
{
    public class RatesViewModel : BaseInpc
    {
        private readonly IRatesRepository repository;
        public ObservableCollection<RateDto> Rates { get; private set; }

        public RatesViewModel(IRatesRepository model)
        {
            this.repository = model;
            Rates = new ObservableCollection<RateDto>(this.repository.GetCurrentRates());
            model.RatesCnahged += Model_RatesCnahged;
        }

        private void Model_RatesCnahged(object sender, RatesAction action, System.Collections.Generic.IEnumerable<RateDto> newRates)
        {
            switch (action)
            {
                case RatesAction.AddedOrChanged:
                    foreach (var rate in newRates)
                        Rates.ReplaceOrAdd(r => r.Base == rate.Base && r.Currency == rate.Currency, rate);
                    break;
                case RatesAction.Clear:
                    Rates.Clear();
                    break;
            }
        }
    }
}
