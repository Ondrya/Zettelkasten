using System.Collections.ObjectModel;
using System.Windows.Shapes;
using Zettelkasten.Applications.Interfaces;
using Zettelkasten.Applications.Services;
using Zettelkasten.DesktopApp.Services;
using Zettelkasten.Domain.Models;
using Zettelkasten.Domain.Models.Painting;

namespace Zettelkasten.DesktopApp.ViewModels
{
    public partial class 
        ApplicationViewModel : ViewModelBase
    {
        private readonly NoteService _noteService;
        private readonly IGeneticService _geneticService;
        private readonly TagService _tagService;
        private readonly DrawingService _drawingService;
        private readonly bool _showDebugMessage;

        public ApplicationViewModel()
        {
            _showDebugMessage = false;
            _noteService = new NoteService();
            _geneticService = new GeneticService(); 
            _tagService = new TagService();
            CanvasWidth = 600;
            CanvasHeight = 600;
            _drawingService = new DrawingService(10, 20, CanvasWidth / 2, CanvasHeight / 2);
        }


        public ZettelNoteNew ZettelNoteNew { get; set; } = new ZettelNoteNew();
        
        public ObservableCollection<NoteListLookUp> ZettelList { get; set; } = new ObservableCollection<NoteListLookUp>();

        public List<Shape> _figures = new List<Shape>();
        public ObservableCollection<Shape> Figures { get; set; } = 
            new ObservableCollection<Shape>();

        public List<List<PolarPointPolyColored>> Selection { get; set; } = new List<List<PolarPointPolyColored>>();

        public List<string> TagCollection { get; set; }

        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }




        public bool IsVisibleTabZettelkastenNew { get; set; }

        public bool IsVisibleTabSearch { get; set; }

        public bool IsVisibleTabPlanning { get; set; }

        public bool IsVisibleTabZettelkasten { get; set; }

        public bool IsVisibleTabSelectionMap { get; set; }

        public bool IsVisibleTabNotepad { get; set; }
    }
}
