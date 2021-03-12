using DtoTypes;
using System.Collections.Generic;

namespace Common
{
    public class CurrencySettingsModel
    {
        public CurrencyDto SelectedCurrensy;
        public IList<CurrencyDto> AvailableCurrencyList;

        public CurrencySettingsModel(CurrencyDto selectedCurrensy,IList<CurrencyDto> availableCurrencyList)
        {
            SelectedCurrensy = selectedCurrensy; AvailableCurrencyList = availableCurrencyList;
        }
    }
}
