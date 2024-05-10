namespace Zettelkasten.Domain.Models
{
    /// <summary>
    /// ЛукАп записи
    /// </summary>
    public class NoteListLookUp : ModelBase
    {
        public int ParentNoteId { get; set; }
        public string Tags { get; set; }
    }
}
