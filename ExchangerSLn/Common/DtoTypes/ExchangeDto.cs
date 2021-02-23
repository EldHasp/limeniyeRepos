namespace DtoTypes
{
    /// <summary>Предложение по обмену базовой валюты.</summary>
    public class ExchangeDto
    {
        /// <summary>Информация по курсу и валютам.</summary>
        public RateDto Rate { get; }

        /// <summary>Сумма базовой валюты.</summary>
        /// <remarks>Если пользователь внёс 80 рублей, то тут будет 80.0 рублей. То есть доступный баланс для обмена.</remarks>
        public decimal AmounBase { get; }

        /// <summary>Сумма валюты на которую обмениваеся.</summary>
        /// <remarks>Если пользователь внёс 80 рублей, то тут будет 1.0 долар</remarks>
        public decimal Available { get; }

        /// <summary>Сумма базовой валюты недостающий до ближайшей.</summary>
        /// <remarks>Если пользователь внёс 80 рублей, то при курсе 1 USD = 70.38 RUB Lack будет хранить значение 68.76, то есть столько, сколько не хвататет до следующей единицы.</remarks>
        public decimal Lack { get; }

        public ExchangeDto(RateDto rate, decimal amounBase, decimal available, decimal lack)
        {
            Rate = rate; AmounBase = amounBase; Available = available; Lack = lack;
        }
    }

}
