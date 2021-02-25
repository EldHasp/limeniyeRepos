using DtoTypes;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Exchanger
{
    public partial class App : Application
    {
        private ReadOnlyDictionary<string, CurrencyDto> available { get; }
            = new ReadOnlyDictionary<string, CurrencyDto>
            (new CurrencyDto[]
                {
                    new CurrencyDto("USD", "$"),
                    new CurrencyDto("EUR", "€"),
                    new CurrencyDto("RUB", "₽"),
                    new CurrencyDto("CNY", "¥"),
                    new CurrencyDto("KZT", "₸"),
                    new CurrencyDto("VND", "₫"),
                    new CurrencyDto("CHF", "₣")
                }
                .ToDictionary(p => p.Symbol)
            );



    }
}
