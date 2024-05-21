using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Zettelkasten.Applications.Services;
using Zettelkasten.Domain.Models;
using Zettelkasten.Domain.Models.Painting;

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
            var fromDb = _noteService.Get().Where(x => x != null);
            foreach (var item in fromDb)
            {
                if (item.Tags == null)
                    item.Tags = new List<string>();
            }
            return fromDb;
        }

        private RelayCommand refreshZettelkastenCommand;
        public ICommand RefreshZettelkastenCommand => refreshZettelkastenCommand ??= new RelayCommand(RefreshZettelkasten);


        private void RefreshZettelkasten(object commandParameter)
        {
            var notes = GetNotes();
            var tagCount = _tagService.GetTagsCount(notes);

            if (_showDebugMessage)
            {
                var msg = $"В хранилище {notes.Count()} записей. Список тегов: {Environment.NewLine}{Environment.NewLine}{string.Join($";{Environment.NewLine}", tagCount.Select(x => $"{x.Key}: {x.Value.Count.ToString()} шт"))}";

                MessageBox.Show(msg);
            }


            var sectorCount = tagCount.Select(x => x.Value.Count).Sum();
            var sectorAngle = 360 / (double)sectorCount;
            var sectorAngles = tagCount.Select(x => (x.Key, x.Value.Count * sectorAngle)).ToDictionary(x => x.Key, x => x.Item2);
            var checkAngles = sectorAngles.Select(x => x.Value).Sum();

            if (_showDebugMessage)
            {
                var msg2 = $"Список секторов: {checkAngles} градусов = {Environment.NewLine}{Environment.NewLine}{string.Join($";{Environment.NewLine}", sectorAngles.Select(x => $"{x.Key}: {x.Value.ToString()} Градусов"))}";
                MessageBox.Show(msg2);
            }

            List<PolarPointPolyColored> points = _geneticService.CreatePopulationFirst(tagCount, notes.ToList());
            //DrawNotes(points);

            var childCount = 4;
            var generationCount = 25;
            var filterAfter = 4;

            Selection = _geneticService.Selection(points, childCount, generationCount, filterAfter);
            var first = Selection[0];


            DrawNotes(first);
        }

        private void DrawNotes(List<PolarPointPolyColored> noteCollection)
        {
            var _figures = _drawingService.CreatePolygones(noteCollection);
            Figures = new ObservableCollection<Polygon>(_figures);
        }

        private RelayCommand nextFromSelectionZettelListCommand;
        public ICommand NextFromSelectionZettelListCommand => nextFromSelectionZettelListCommand ??= new RelayCommand(NextFromSelectionZettelList, (obj) => Selection != null && Selection.Count > 1);

        private void NextFromSelectionZettelList(object commandParameter)
        {
            var index = rnd.Next(Selection.Count);

            var _figures = _drawingService.CreatePolygones(Selection[index]);

            Figures = new ObservableCollection<Polygon>(_figures);
        }

        private Random rnd = new Random();
    }
}
