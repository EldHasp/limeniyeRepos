﻿using System.Collections.Generic;

namespace DtoTypes
{
    /// <summary>Тип для описания валюты.</summary>
    public class CurrencyDto
    {
        /// <summary>Международный код валюты: "USD", "RUB" и т.п.</summary>
        public string Symbol { get; }

        /// <summary>Графический символ валюты, если он есть: "$", "₽" и т.п.<br/>
        /// Если знака нет, то <see langword="null"/> или <see cref="string.Empty"/>.</summary>
        public string Sign { get; }

        /// <summary>Полное наименование валюты: "Доллар США", "Российский Рубль" и т.п.</summary>
        public string FullName { get; }

        /// <summary>Дополнительное описание валюты.</summary>
        public string Description { get; }

        /// <summary>Создаёт экземпляр валюты.</summary>
        /// <param name="symbol">Значение <see cref="Symbol"/>.</param>
        /// <param name="sign">Значение <see cref="Sign"/>.</param>
        public CurrencyDto(string symbol, string sign)
        {
            Symbol = symbol;
            Sign = sign;
        }

        /// <summary>Создаёт экземпляр валюты.</summary>
        /// <param name="symbol">Значение <see cref="Symbol"/>.</param>
        /// <param name="sign">Значение <see cref="Sign"/>.</param>
        /// <param name="fullName">Значение <see cref="FullName"/>.</param>
        public CurrencyDto(string symbol, string sign, string fullName)
            : this(symbol, sign)
        {
            FullName = fullName;
        }

        /// <summary>Создаёт экземпляр валюты.</summary>
        /// <param name="symbol">Значение <see cref="Symbol"/>.</param>
        /// <param name="sign">Значение <see cref="Sign"/>.</param>
        /// <param name="fullName">Значение <see cref="FullName"/>.</param>
        /// <param name="description">Значение <see cref="Description"/>.</param>
        public CurrencyDto(string symbol, string sign, string fullName, string description) : this(symbol, sign, fullName)
        {
            Description = description;
        }

        public override string ToString()
            => $"{{\"{Symbol}\", \"{Sign}\"}}";

        public override int GetHashCode()
        {
            var hashCode = 1038968261;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Symbol);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Sign);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FullName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }
    }
}
