using DtoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventsArgs
{

    public enum RatesAction
    {
        Clear,
        AddedOrChanged
    }

    public delegate void RatesCnahgedHandler(object sender, RatesAction action, IEnumerable<RateDto> newRates);
}
