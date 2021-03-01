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
using Common.Interfaces.Model;

namespace ViewModel.Currency
{
    public class RatesViewModel : BaseViewModel, IRatesViewModel
    {
        private readonly IRatesModel model;
        public ObservableCollection<RateDto> Rates { get; private set; }

        public RatesViewModel(IRatesModel model)
        {
            this.model = model;
            Rates = new ObservableCollection<RateDto>(this.model.Rates);
            model.RatesCnahged += OnRatesCnahged;
        }

        private void OnRatesCnahged(object sender, ChangedAction action, IEnumerable<RateDto> newRates)
        {
            switch (action)
            {
                case ChangedAction.AddedOrChanged:
                    if (Dispatcher.CheckAccess())
                        AddToCollection(newRates);
                    else
                        Dispatcher.BeginInvoke((Action)(() => AddToCollection(newRates)));
                    break;
                case ChangedAction.Clear:
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
