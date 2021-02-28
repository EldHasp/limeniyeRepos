using System.Windows.Threading;

namespace Common.Dispatchers
{
    /// <summary>Интерфейс для типов нуждающихся в <see cref="Dispatcher"/>.</summary>
    public interface IDispatcher
    {
        /// <summary>Диспетчер экземпляра.
        /// Если равен <see langword="null"/>, 
        /// то экземпляру безразлично в каком потоке его использую.</summary>
        Dispatcher Dispatcher { get; }
    }

}
