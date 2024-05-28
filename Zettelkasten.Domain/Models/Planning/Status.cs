using System.Drawing;

namespace Zettelkasten.Domain.Models.Planning
{
    /// <summary>
    /// Статус элемента планирования
    /// </summary>
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }



        public static Status None()
        {
            return new Status { Id = 0, Name = "", Color = Color.Gray };
        }

        public static Status InWork()
        {
            return new Status { Id = 1, Name = "В работе", Color = Color.Orange };
        }

        public static Status Block()
        {
            return new Status { Id = 2, Name = "Блок", Color = Color.Red};
        }

        public static Status Ready()
        {
            return new Status { Id = 3, Name = "Готово", Color = Color.Green};
        }


        private static List<Status>? _collection;

        public static List<Status> Collection()
        {
            if (_collection == null)
            {
                _collection = new List<Status>() 
                { 
                    None(), InWork(), Block(), Ready(),
                };
            }

            return _collection;
        }
    }
}
