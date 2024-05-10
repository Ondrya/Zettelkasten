namespace Zettelkasten.Domain.Models
{
    /// <summary>
    /// Вложение к записи
    /// </summary>
    public class AttachmentNote : ModelBase
    {
        /// <summary>
        /// Путь
        /// </summary>
        public string Path { get; set; }
    }
}