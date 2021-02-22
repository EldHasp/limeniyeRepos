namespace DtoTypes
{
    /// <summary>Предложение по обмену базовой валюты.</summary>
    public class ExchangeDto
    {
        /// <summary>Информация по курсу и валютам.</summary>
        public RateDto Rate { get; }

        /// <summary>Сумма базовой валюты.</summary>
        public decimal AmounBase { get; }
 
        /// <summary>Сумма валюты на которую обмениваеся.</summary>
        public decimal Amoun { get; }
  
        /// <summary>Сумма базовой валюты недостающий до ближайшей 
        /// "ровной" суммы получаемой.</summary>
        public decimal Lack { get; }
  }

}
