using DtoTypes;
using System.Collections.Generic;

namespace ExchangerModels
{
    public partial class ExchangerModel
    {
        private IList<ExchangeDto> exchenges;
        public IReadOnlyCollection<ExchangeDto> Exchanges { get; private set; }
    }
}
