using Common;
using Common.Enums;
using Common.EventsArgs;
using DtoTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExchangerModels
{
    public partial class ExchangerModel
    {
        #region Событие и методы изменения Exchages
        /// <summary>Событие извещающее о изменении производных предложений обмена.
        public event ExchangesCnahgedHandler ExchangesCnahged;

        /// <summary>Приватный метод очистки производных предложений обмена.</summary>
        private void ClearExchenges()
        {
            exchenges.Clear();
            ExchangesCnahged?.Invoke(this, ChangedAction.Clear, null);
        }

        /// <summary>Добавление или замена одного производного предложения обмена.</summary>
        /// <param name="addRate">Новый или изменённое предложение.</param>
        private void AddExchanges(ExchangeDto addExchange)
        {
            exchenges.ReplaceOrAdd(r => r.Id == addExchange.Id, addExchange);
            ExchangesCnahged?.Invoke(this, ChangedAction.AddedOrChanged, new ExchangeDto[] { addExchange });
        }

        /// <summary>Добавление или замена нескольких производных предложений обмена.</summary>
        /// <param name="addRate">Новые или изменённые курсы.</param>
        private void AddRangeExchenges(IEnumerable<ExchangeDto> addExchanges)
        {
            foreach (var exchange in addExchanges)
                exchenges.ReplaceOrAdd(r => r.Id == exchange.Id, exchange);

            ExchangesCnahged?.Invoke(this, ChangedAction.AddedOrChanged, addExchanges.GetEnumerable());
        }
        #endregion
    }
}
