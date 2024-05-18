using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace Zettelkasten.Applications.Services
{
    /// <summary>
    /// Копирование сложных типов без ссылок
    /// </summary>
    public static class ExtensionService
    {
        public static T DeepCopy<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }


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
