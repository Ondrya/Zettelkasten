using Newtonsoft.Json;
using System.Windows;
using Zettelkasten.Domain.Models;

namespace Zettelkasten.DesktopApp.ViewModels
{
    public partial class ApplicationViewModel : ViewModelBase
    {
        private RelayCommand сreateZettelNote;
        public RelayCommand CreateZettelNote
        {
            get
            {
                return сreateZettelNote ??
                    (сreateZettelNote = new RelayCommand(obj =>
                    {
                        var msg = JsonConvert.SerializeObject(ZettelNoteNew, Formatting.Indented);
                        var answer = MessageBox.Show(msg, "Добавить новую запись?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (answer == MessageBoxResult.No)
                        {
                            ZettelNoteNew.Clear();
                            return;
                        }
                            
                        var note = new Note();
                        note.Name = ZettelNoteNew.Name;
                        note.CreatedAt = ZettelNoteNew.CreatedAt;
                        if (!string.IsNullOrWhiteSpace(ZettelNoteNew.Tag))
                            note.Tags = ZettelNoteNew.Tag.Split(",").Select(x => x.Trim()).ToList();
                        note.Content = ZettelNoteNew.Content;

                        var noteId = _noteService.Create(note);

                        MessageBox.Show($"Создана новая запись #{noteId}", "Сохранено");

                        ZettelNoteNew.Clear();
                    },
                    (obj => ZettelNoteNew != null && ZettelNoteNew.IsValid())
                    ));
            }
        }

    }
}
