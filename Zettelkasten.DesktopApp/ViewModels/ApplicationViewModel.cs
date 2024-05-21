using System.Collections.ObjectModel;
using System.Windows.Shapes;
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
        private readonly GeneticService _geneticService;
        private readonly TagService _tagService;
        private readonly DrawingService _drawingService;

        public ApplicationViewModel()
        {
            _noteService = new NoteService();
            _geneticService = new GeneticService(); 
            _tagService = new TagService();
            CanvasWidth = 600;
            CanvasHeight = 600;
            _drawingService = new DrawingService(10, 20, CanvasWidth / 2, CanvasHeight / 2);
        }


        public ZettelNoteNew ZettelNoteNew { get; set; } = new ZettelNoteNew();
        
        public ObservableCollection<NoteListLookUp> ZettelList { get; set; } = new ObservableCollection<NoteListLookUp>();

        public ObservableCollection<Polygon> Figures { get; set; } = 
            new ObservableCollection<Polygon>();

        public List<List<PolarPointPolyColored>> Selection { get; set; } = new List<List<PolarPointPolyColored>>();

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
