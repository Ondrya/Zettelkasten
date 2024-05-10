using System.Collections.ObjectModel;

namespace Zettelkasten.Domain.Models
{
    /// <summary>
    /// Узлы для иерархического просмотра
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Узловая запись
        /// </summary>
        public NoteLookUp ParentNode { get; set; }

        /// <summary>
        /// Вложенные записи
        /// </summary>
        public ObservableCollection<NoteLookUp> ChildNodes { get; set; }
    }
}
