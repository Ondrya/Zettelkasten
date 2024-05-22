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


        private RelayCommand updateZetteleNote;
        public ICommand UpdateZetteleNote => updateZetteleNote ??= new RelayCommand(PerformUpdateZettelNote, (obj) => ZettelNoteEdit != null && ZettelNoteEdit.IsValid());
        private void PerformUpdateZettelNote(object obj)
        {
            var msg = JsonConvert.SerializeObject(ZettelNoteEdit, Formatting.Indented);
            var answer = MessageBox.Show(msg, "Перезаписать запись?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (answer == MessageBoxResult.No)
            {
                ZettelNoteEdit.Clear();
                return;
            }

            var note = new Note();
            note.Id = ZettelNoteEdit.Id;
            note.Name = ZettelNoteEdit.Name;
            note.CreatedAt = ZettelNoteEdit.CreatedAt;
            if (!string.IsNullOrWhiteSpace(ZettelNoteEdit.Tag))
                note.Tags = ZettelNoteEdit.Tag.Split(",").Select(x => x.Trim()).ToList();
            note.Content = ZettelNoteEdit.Content;

            _noteService.Update(note);

            MessageBox.Show($"Обновлена запись #{note.Id}", "Сохранено");

            ZettelNoteEdit.Clear();
            ShowTab(MenuItems.Search);
        }


        private RelayCommand deleteZetteleNote;
        public ICommand DeleteZetteleNote => deleteZetteleNote ??= new RelayCommand(PerformDeleteZettelNote, (obj) => ZettelNoteEdit != null && ZettelNoteEdit.IsValid());
        private void PerformDeleteZettelNote(object obj)
        {
            var msg = JsonConvert.SerializeObject(ZettelNoteEdit, Formatting.Indented);
            var answer = MessageBox.Show(msg, "Удалить запись?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (answer == MessageBoxResult.No)
            {
                ZettelNoteEdit.Clear();
                return;
            }

            _noteService.Delete(ZettelNoteEdit.Id);

            MessageBox.Show($"Удалена запись #{ZettelNoteEdit.Id}", "Удалено");

            ZettelNoteEdit.Clear();
            ShowTab(MenuItems.Search);
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
            IsProgressBarVisible = true;

            var notes = GetNotes();
            var tagCount = _tagService.GetTagsCount(notes);
            TagCollection = tagCount.Select(x => x.Key).ToList();

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

                        Selection = _geneticService.Selection(points, ChildCount, GenerationCount, FilterAfter);
            var first = Selection[0];
            _figures = _drawingService.CreatePolygones(first);

            DrawNotes(_figures);

            IsProgressBarVisible = false;
        }

        public List<Shape> CreateLinks(List<Shape> polygons)
        {
            var shapeLinks = new List<Shape>();

            foreach (var item in polygons)
            {
                var tags = GetShapeTags(item);
                var theSameTags = polygons
                    .Where(x => 
                        GetShapeTags(x).Intersect(tags).Any())
                    .ToList();
                foreach (var item1 in theSameTags)
                {
                    var itemLink = new Line();

                    // Create a red Brush  
                    SolidColorBrush redBrush = new SolidColorBrush();
                    redBrush.Color = Colors.Red;

                    // Set Line's width and color  
                    itemLink.StrokeThickness = 1;
                    itemLink.Stroke = redBrush;

                    var centered = 10;
                    itemLink.X1 = (item as Polygon).Points[0].X + centered;
                    itemLink.Y1 = (item as Polygon).Points[0].Y + centered;
                    itemLink.X2 = (item1 as Polygon).Points[0].X + centered;
                    itemLink.Y2 = (item1 as Polygon).Points[0].Y + centered;

                    shapeLinks.Add(itemLink);
                }

            }
            return shapeLinks;
        }

        private static List<string> GetShapeTags(Shape item)
        {
            return (item.ToolTip as string).Split("---")[0].Split(";").ToList();
        }

        public void DrawNotes(List<Shape> polygons)
        {
            var res = polygons;
            var shapeLinks = CreateLinks(polygons);
            res.AddRange(shapeLinks);

            Figures = new ObservableCollection<Shape>(res);
        }

        public void ClearDrawNotes()
        {
            Figures = new ObservableCollection<Shape>();
        }

        private RelayCommand nextFromSelectionZettelListCommand;
        public ICommand NextFromSelectionZettelListCommand => nextFromSelectionZettelListCommand ??= new RelayCommand(NextFromSelectionZettelList, (obj) => Selection != null && Selection.Count > 1);

        private void NextFromSelectionZettelList(object commandParameter)
        {
            var index = rnd.Next(Selection.Count);

            var _figures = _drawingService.CreatePolygones(Selection[index]);

            DrawNotes(_figures);
        }

        private Random rnd = new Random();


        private RelayCommand doubleClickCommand;
        public ICommand DoubleClickCommand => doubleClickCommand ??= new RelayCommand(DoubleClick);

        private void DoubleClick(object commandParameter)
        {
            //MessageBox.Show($"{SelectedNoteListLookUp.Id} --- {SelectedNoteListLookUp.Name}");
            ZettelNoteEdit = new ZettelNoteEdit(_noteService.Get(SelectedNoteListLookUp.Id));
            ShowTab(MenuItems.ZettelkastenEdit);
        }

    }
}
