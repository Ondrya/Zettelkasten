namespace Zettelkasten.Domain.Models
{
    /// <summary>
    /// Связь между записями
    /// </summary>
    public class NoteConnection : ModelBase
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int NoteIdFrom { get; set; }

        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int NoteIdTo { get; set; }
    }
}