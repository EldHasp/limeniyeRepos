using DtoTypes;
using System.Collections.Generic;

namespace TempDemostratedProject
{
    public class Model
    {
        public IReadOnlyCollection<RateDto> Rates = new List<RateDto>()
        {
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),
            new RateDto(new CurrencyDto("EUR", "€"), new CurrencyDto("UAH", "₴"), 33.75214124m)
        };
    }
}
