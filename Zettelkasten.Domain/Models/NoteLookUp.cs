namespace Zettelkasten.Domain.Models
{
    /// <summary>
    /// ЛукАп записи
    /// </summary>
    public class NoteLookUp : ModelBase
    {
        public int ParentNoteId { get; set; }
    }
}
