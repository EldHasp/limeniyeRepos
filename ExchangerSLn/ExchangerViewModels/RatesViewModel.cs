using Common.EventsArgs;
using System.Collections.ObjectModel;
using DtoTypes;
using Common.Interfaces.Repository;
using Common;
using Common.Dispatchers;
using System;
using System.Collections.Generic;
using Common.Interfaces.ViewModel;
using Common.Enums;

namespace ViewModel.Currency
{
    public class RatesViewModel : BaseViewModel, IRatesViewModel
    {
        private readonly IRatesRepository repository;
        public ObservableCollection<RateDto> Rates { get; private set; }

        public RatesViewModel(IRatesRepository model)
        {
            this.repository = model;
            Rates = new ObservableCollection<RateDto>(this.repository.GetCurrentRates());
            model.RatesCnahged += Model_RatesCnahged;
        }

        private void Model_RatesCnahged(object sender, RatesAction action, IEnumerable<RateDto> newRates)
        {
            switch (action)
            {
                case RatesAction.AddedOrChanged:
                    if (Dispatcher.CheckAccess())
                        AddToCollection(newRates);
                    else
                        Dispatcher.BeginInvoke((Action)(() => AddToCollection(newRates)));
                    break;
                case RatesAction.Clear:
                    if (Dispatcher.CheckAccess())
                        Rates.Clear();
                    else
                        Dispatcher.BeginInvoke((Action)(() => Rates.Clear()));
                    break;
            }
        }

        private void AddToCollection(IEnumerable<RateDto> newRates)
        {
            foreach (var rate in newRates)
                Rates.ReplaceOrAdd(r => r.Base == rate.Base && r.Currency == rate.Currency, rate);
        }
    }
}
