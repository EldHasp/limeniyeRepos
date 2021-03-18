using System.Collections.Generic;

namespace DtoTypes
{
    /// <summary>Курс пары валют.</summary>
    public class RateDto
    {
        public int Id { get; }
        /// <summary>Оцениваемая валюта (числитель).</summary>
        public CurrencyDto Currency { get; }

        /// <summary>Базовая валюта (Знаменатель).</summary>
        public CurrencyDto Base { get; }

        /// <summary>Отнощение <see cref="Currency"/> к <see cref="Base"/> (Currency/Base).</summary>
        public decimal Rate { get; }

        /// <summary>Создаёт экземпляр Курса пары.</summary>
        /// <param name="base">Базовая валюта (Знаменатель).</param>
        /// <param name="currency">Оцениваемая валюта (числитель).</param>
        /// <param name="rate">Отнощение <see cref="Currency"/> к <see cref="Base"/> (Currency/Base).</param>
        public RateDto(CurrencyDto currency, CurrencyDto @base, decimal rate)
        {
            Currency = currency;
            Base = @base;
            Rate = rate;

            Id = GetHashCode();
        }

        public override string ToString()
            => $"{{{Rate} {Currency.Symbol}/{Base.Symbol}}}";

        public override int GetHashCode()
        {
            var hashCode = -1518519874;
            hashCode = hashCode * -1521134295 + EqualityComparer<CurrencyDto>.Default.GetHashCode(Currency);
            hashCode = hashCode * -1521134295 + EqualityComparer<CurrencyDto>.Default.GetHashCode(Base);
            return hashCode;
        }
    }

}
