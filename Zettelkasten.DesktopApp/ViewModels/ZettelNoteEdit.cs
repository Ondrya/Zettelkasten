using Zettelkasten.Domain.Models;

namespace Zettelkasten.DesktopApp.ViewModels
{
    public class ZettelNoteEdit : ViewModelBase
    {
        public ZettelNoteEdit(Note note)
        {
            this.Id = note.Id;
            this.Content = note.Content;
            this.CreatedAt = note.CreatedAt;
            this.Name = note.Name;
            this.Tag = string.Join(";", note.Tags);
        }

        public void Clear()
        {
            
        }

        public bool IsValid()
        {
            return
                (!string.IsNullOrWhiteSpace(Name));
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }
    }
}
