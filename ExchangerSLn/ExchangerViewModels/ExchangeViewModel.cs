using DtoTypes;
using ExchangerModels.Interface;
using ExchangerViewModels.Intefaces;
using Simplified;
using System.Collections.ObjectModel;
using System.Linq;

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
