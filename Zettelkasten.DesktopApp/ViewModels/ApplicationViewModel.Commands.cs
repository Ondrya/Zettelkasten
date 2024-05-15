using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
            return _noteService.Get().Where(x => x != null);
        }

        private RelayCommand refreshZettelkastenCommand;
        public ICommand RefreshZettelkastenCommand => refreshZettelkastenCommand ??= new RelayCommand(RefreshZettelkasten);


        private void RefreshZettelkasten(object commandParameter)
        {
            var tagCount = GetTagsCount();
            var sectorCount = tagCount.Select(x => x.Value.Count).Sum();
            var sectorAngle = 360 / (double)sectorCount;
            var sectorAngles = tagCount.Select(x => (x.Key, x.Value.Count * sectorAngle)).ToDictionary(x => x.Key, x => x.Item2);
            var checkAngles = sectorAngles.Select(x => x.Value).Sum();

            var msg = $"Список секторов: {checkAngles} градусов = {Environment.NewLine}{Environment.NewLine}{string.Join($";{Environment.NewLine}", sectorAngles.Select(x => $"{x.Key}: {x.Value.ToString()} Градусов"))}";
            MessageBox.Show(msg);


            Random random = new Random();
            var tagColors = tagCount.Select(x => (x.Key, Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)))).ToDictionary(x => x.Key, x => x.Item2);

            // todo раскидать точки и потом рандомно двигать по одной

            // создаём точки

            var notes = GetNotes().ToList();
            var noteCount = notes.Count;
            var noteAngle = 360 / (double)noteCount;
            var radius = 200;
            double angle = 0.0;
            var points = new List<PolarPointPolyColored>();

            foreach (var note in notes)
            {
                var noteTags = note.Tags;
                if (noteTags.Count == 0)
                    note.Tags = new List<string>() { noTagPlaceholder };
                var colors = tagColors.Where(kvp => note.Tags.Contains(kvp.Key)).Select(x => x.Value).ToList();
                var point = new PolarPointPolyColored(radius, angle, colors, note.Id, note.Name);
                points.Add(point);

                // prepare next step
                angle = angle + noteAngle;
            }

            double probe = CheckCollection(points);
        }


        /// <summary>
        /// Проба дляя генетического алгоритма
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private double CheckCollection(List<PolarPointPolyColored> points)
        {
            var colors = points.Select(x => x.Colors).SelectMany(x => x).Distinct().ToList(); // список всех цветов
            var tagSectorDiffs = new List<(double, double)>();
            foreach (var color in colors)
            {
                var filteredPoints = points.Where(x => x.Colors.Contains(color)).OrderBy(x => x.AngleDeg).ToList();
                var min = filteredPoints.Min(x => x.AngleDeg);
                var max = filteredPoints.Max(x => x.AngleDeg);
                tagSectorDiffs.Add((min, max));
            }

            return tagSectorDiffs.Select(x => x.Item2 - x.Item1).Sum();
        }


        private void MutateCollection(List<PolarPointPolyColored> points)
        {
            // todo сделать 4 вариации, поменяв местами 2 точки
            // todo повторить 10 раз
            // todo убить всех потомков кроме 4 с лучшей апроксимацией, сохранив первоначального родителя
        }


        private string noTagPlaceholder = "без тэга";


        private Dictionary<string, List<int>> GetTagsCount()
        {
            var noteTags = GetNotes().Select(x => new { x.Id, x.Tags }).ToList();
            var tags = new Dictionary<string, List<int>>();
            
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

            var msg = $"В хранилище {noteTags.Count} записей. Список тегов: {Environment.NewLine}{Environment.NewLine}{string.Join($";{Environment.NewLine}", tags.Select(x => $"{x.Key}: {x.Value.Count.ToString()} шт"))}";
            MessageBox.Show(msg);

            return tags;
        }
    }
}
