using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ExtensionMethods
    {
        /// <summary>Метод заменяющий элемент в индексированной коллекции.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="list">Индексированная коллекция.</param>
        /// <param name="predicate">Предикат для поиска элемента в коллекции, котороый надо заменить.</param>
        /// <param name="newItem">Элемент на который будет заменён найденный элемент.</param>
        /// <returns><see langword="true"/> если элемент был найден и заменён, иначе - <see langword="false"/>.</returns>
        public static bool Replace<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list[i] = newItem;
                    return true;
                }
            }
            return false;
        }

        /// <summary>Метод заменяющий или добавляющий элемент в индексированную коллекции.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="list">Индексированная коллекция.</param>
        /// <param name="predicate">Предикат для поиска элемента в коллекции, котороый надо заменить.</param>
        /// <param name="newItem">Элемент на который будет заменён найденный элемент.</param>
        /// <returns><see langword="true"/> если элемент был найден и заменён,
        /// <see langword="false"/> - элемент не был найден и <paramref name="newItem"/> добаяляется в коллекцию.</returns>
        public static bool ReplaceOrAdd<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            if (list.Replace(predicate, newItem))
                return true;

            list.Add(newItem);
            return false;
        }

        /// <summary>Удаляет первый элемент в индексированной коллекцию,
        /// удовлетворяющий предикату <paramref name="predicate"/>.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="list">Индексированная коллекция.</param>
        /// <param name="predicate">Предикат для поиска элемента в коллекции, котороый надо удалить.</param>
        /// <returns><see langword="true"/> если элемент был найден и удалён, иначе - <see langword="false"/>.</returns>
        public static bool RemoveFirst<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>Возвращает коллекцию поэлементно.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="collection">Исходная коллекция.</param>
        /// <returns>Последовательность элементов коллекции.</returns>
        /// <remarks>Используется для защиты исходной коллекции от изменений.</remarks>
        public static IEnumerable<T> GetEnumerable<T> (this IEnumerable<T> collection)
        {
            foreach (T item in collection)
                yield return item;
        }
    }
}
