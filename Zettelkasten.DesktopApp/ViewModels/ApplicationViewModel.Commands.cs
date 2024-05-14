using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zettelkasten.Domain.Models;

namespace Zettelkasten.DesktopApp.ViewModels
{
    public partial class ApplicationViewModel : ViewModelBase
    {
        private RelayCommand createZetteleNote;
        public ICommand CreateZetteleNote => createZetteleNote ??= new RelayCommand(PerformCreateZettelNote, (obj) => ZettelNoteNew != null && ZettelNoteNew.IsValid());
        private void PerformCreateZettelNote(object obj)
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
        }


        private RelayCommand refreshZettelListCommand;
        public ICommand RefreshZettelListCommand => refreshZettelListCommand ??= new RelayCommand(RefreshZettelList);
        private void RefreshZettelList(object commandParameter)
        {
            RefreshZettelListInner();
        }

        private void RefreshZettelListInner()
        {
            var ls = GetNotes().Select(x => x.ToListLookUp());
            ZettelList = new ObservableCollection<NoteListLookUp>(ls);
        }

        private IEnumerable<Note> GetNotes()
        {
            return _noteService.Get().Where(x => x != null);
        }

        private RelayCommand refreshZettelkastenCommand;
        public ICommand RefreshZettelkastenCommand => refreshZettelkastenCommand ??= new RelayCommand(RefreshZettelkasten);
        private void RefreshZettelkasten(object commandParameter)
        {
            GetTagsCount();
        }

        private void GetTagsCount()
        {
            var noteTags = GetNotes().Select(x => new { x.Id, x.Tags }).ToList();
            var tags = new Dictionary<string, List<int>>();
            var noTagPlaceholder = "без тэга";

            tags.Add(noTagPlaceholder, new List<int>());

            foreach (var noteTag in noteTags)
            {
                var keys = noteTag.Tags;
                if (keys == null)
                {
                    tags[noTagPlaceholder].Add(noteTag.Id);
                }
                else
                {
                    foreach (var item in keys)
                    {
                        if (tags.ContainsKey(item))
                        {
                            tags[item].Add(noteTag.Id);
                        }
                        else
                        {
                            tags.Add(item, new List<int>() { noteTag.Id });
                        }
                    }
            }
        }

            var msgInfo = $"В хранилище {noteTags.Count} записей. Список тегов: {Environment.NewLine}{Environment.NewLine}{string.Join($";{Environment.NewLine}", tags.Select(x => $"{x.Key}: {x.Value.Count.ToString()} шт"))}";
            MessageBox.Show(msgInfo);
        }
    }
}
