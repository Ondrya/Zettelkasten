using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace Zettelkasten.Applications.Services
{
    
    public static class ExtensionService
    {
        /// <summary>
        /// Копирование сложных типов без ссылок
        /// </summary>
        public static T DeepCopy<T>(T item)
        {
            var json = JsonConvert.SerializeObject(item);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Копирование коллекции сложных типов без ссылок
        /// </summary>
        public static IList<T> DeepCopyList<T>(this IList<T> list)
        {
            var json = JsonConvert.SerializeObject(list);
            return JsonConvert.DeserializeObject<IList<T>>(json);
        }


        /// <summary>
        /// Поменять местами 2 элемента
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        /// <returns></returns>
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
    }
}
