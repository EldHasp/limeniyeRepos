using System.Linq;
using Common.Events;
using System.Collections.ObjectModel;
using Simplified;
using DtoTypes;
using Common.Enums;
using ExchangerViewModels;
using Common.Interfaces.Model;
using Common.Interfaces.ViewModel;

namespace ViewModel.Currency
{
    public class ExchangerListViewModel : BaseInpc, IExchangerListViewModel
    {
        private readonly IExchangerModel model;

        private IExchangeViewModel _selectedExchange;


        public ObservableCollection<IExchangeViewModel> Exchanges { get; } = new ObservableCollection<IExchangeViewModel>();
        /// <summary> Выбрана валюта для выдачи валюты. </summary>
        public IExchangeViewModel SelectedExchange { get => _selectedExchange; set => Set(ref _selectedExchange, value); }


        private void ActionListChanged(object sender, NotifyDictionaryChangedEventArgs<int, ExchangeDto> e)
        {

            switch (e.Action)
            {
                case ActionListEnum.Added:
                    // Если такой пары не существует -- добавить новыую пару уже с готовыми значениями. 
                    if ((Exchanges.FirstOrDefault(x => (x.ExchangeDto.Rate.Base == e.NewValue.Rate.Base) && (x.ExchangeDto.Rate.Currency == e.NewValue.Rate.Currency)) == null))
                        Exchanges.Add(new ExchangeViewModel(e.NewValue));
                    break;
                case ActionListEnum.Changed:
                    // Если существует такая пара -- изменить все её значения
                    Exchanges.First(x => ((x.ExchangeDto.Rate.Base == e.NewValue.Rate.Base) && (x.ExchangeDto.Rate.Currency == e.NewValue.Rate.Currency))).SetNewExchange(e.NewValue);
                    break;
                case ActionListEnum.Removed:
                    // Если существует такая пара -- удалить её
                    Exchanges.Remove(Exchanges.FirstOrDefault(x => (x.ExchangeDto.Rate.Base == e.NewValue.Rate.Base) && (x.ExchangeDto.Rate.Currency == e.NewValue.Rate.Currency)));
                    break;
            }
        }


        public ExchangerListViewModel(IExchangerModel model)
        {
            this.model = model;

            // TODO: Отсутствует начальное заполение списка.

            model.ChangedExchanges += ActionListChanged;
        }
    }
}
