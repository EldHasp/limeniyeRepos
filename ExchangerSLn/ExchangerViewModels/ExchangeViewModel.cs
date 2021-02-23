using Common.Interfaces.ViewModel;
using DtoTypes;
using Simplified;

namespace ExchangerViewModels
{
    public class ExchangeViewModel : BaseInpc, IExchangeViewModel
    {
        #region Private Fields
        private ExchangeDto _exchangeDto;
        #endregion

        public ExchangeDto ExchangeDto { get => _exchangeDto; private set => Set(ref _exchangeDto, value); }

        public void SetNewExchange(ExchangeDto exchangeDto)
        {
            ExchangeDto = exchangeDto;
        }

        public ExchangeViewModel() { }
        public ExchangeViewModel(ExchangeDto dto) => SetNewExchange(dto);
    }
}
