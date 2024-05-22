using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Shapes;
using Zettelkasten.Applications.Interfaces;
using Zettelkasten.Applications.Services;
using Zettelkasten.DesktopApp.Services;
using Zettelkasten.DesktopApp.ViewModels;
using Zettelkasten.Domain.Models;
using Zettelkasten.Domain.Models.Painting;

namespace Zettelkasten.DesktopApp.ViewModels
{
    public partial class 
        ApplicationViewModel : ViewModelBase
    {
        private readonly IStorageService _storageService;
        private readonly INoteService _noteService;
        private readonly IGeneticService _geneticService;
        private readonly TagService _tagService;
        private readonly DrawingService _drawingService;
        private readonly bool _showDebugMessage;

        public ApplicationViewModel()
        {
            _showDebugMessage = false;
            _storageService = new LocalFileStorageService();
            _noteService = new NoteService(_storageService);
            _geneticService = new GeneticService(); 
            _tagService = new TagService();
            CanvasWidth = 600;
            CanvasHeight = 600;
            _drawingService = new DrawingService(10, 20, CanvasWidth / 2, CanvasHeight / 2);
        }


        public ZettelNoteNew ZettelNoteNew { get; set; } = new ZettelNoteNew();

        public ZettelNoteEdit ZettelNoteEdit { get; set; }

        public ObservableCollection<NoteListLookUp> ZettelList { get; set; } = new ObservableCollection<NoteListLookUp>();
        public NoteListLookUp SelectedNoteListLookUp { get; set; }

        public List<Shape> _figures = new List<Shape>();
        public ObservableCollection<Shape> Figures { get; set; } = 
            new ObservableCollection<Shape>();

        public List<List<PolarPointPolyColored>> Selection { get; set; } = new List<List<PolarPointPolyColored>>();

        public List<string> TagCollection { get; set; }

        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }


        public int GenerationCount { get; set; } = 50;
        public int ChildCount { get; set; } = 4;
        public int FilterAfter { get; set; } = 4;


        public bool IsProgressBarVisible { get; set; } = false;

        public bool IsVisibleTabZettelkastenNew { get; set; }

        public bool IsVisibleTabZettelkastenEdit { get; set; }

        public bool IsVisibleTabSearch { get; set; }

        public bool IsVisibleTabPlanning { get; set; }

        public bool IsVisibleTabZettelkasten { get; set; }

        public bool IsVisibleTabSelectionMap { get; set; }

        public bool IsVisibleTabNotepad { get; set; }

    }
}
