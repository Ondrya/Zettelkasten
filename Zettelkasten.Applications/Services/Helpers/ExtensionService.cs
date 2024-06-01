using Newtonsoft.Json;

namespace Zettelkasten.Applications.Services
{
    /// <summary>
    /// Расширения для удобства работы
    /// </summary>
    public static class ExtensionService
    {
        /// <summary>
        /// Копирование сложных типов без ссылок
        /// </summary>
        public static T DeepCopy<T>(T item) where T : class
        {
            var json = JsonConvert.SerializeObject(item);
            var res = JsonConvert.DeserializeObject<T>(json);
            if (res == null)
                throw new InvalidOperationException();
            return res;
        }

        /// <summary>
        /// Копирование коллекции сложных типов без ссылок
        /// </summary>
        public static IList<T> DeepCopyList<T>(this IList<T> list) where T : class
        {
            var json = JsonConvert.SerializeObject(list);
            var res = JsonConvert.DeserializeObject<IList<T>>(json);
            if (res == null)
                throw new InvalidOperationException();
            return res;
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

        /// <summary>
        /// Проверка, что строка содержит одну из подстрок...
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needles"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string haystack, params string[] needles)
        {
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка, что строка содержит одну из подстрок...
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needles"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string haystack, IEnumerable<string> needles)
        {
            if (haystack == null)
                return false;
            if (!needles.Any())
                return false;
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle))
                    return true;
            }

            return false;
        }
    }
}
