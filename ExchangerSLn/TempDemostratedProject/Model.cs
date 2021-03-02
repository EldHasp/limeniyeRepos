using DtoTypes;
using System.Collections.Generic;

namespace TempDemostratedProject
{
    public class Model
    {
        public IReadOnlyCollection<RateDto> Rates = new List<RateDto>()
        {
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//1
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//2
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//3
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//4
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//5
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//6
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//7
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//8
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//9
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//10
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//11
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//12
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//13
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//14
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//15
            new RateDto(new CurrencyDto("USD", "$"), new CurrencyDto("UAH", "₴"), 27.434355m),//16
            new RateDto(new CurrencyDto("EUR", "€"), new CurrencyDto("UAH", "₴"), 33.75214124m)//17
            //17 элементов
        };

        public Model()
        {

        }
    }
}
