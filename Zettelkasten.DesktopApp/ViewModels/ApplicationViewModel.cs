using System.Collections.ObjectModel;
using System.Windows.Shapes;
using Zettelkasten.Applications.Services;
using Zettelkasten.Domain.Models;

namespace Zettelkasten.DesktopApp.ViewModels
{
    public partial class 
        ApplicationViewModel : ViewModelBase
    {
        private readonly INoteService _noteService;
        private readonly GeneticService _geneticService;
        private readonly TagService _tagService;

        public ApplicationViewModel()
        {
            _noteService = new NoteService();
            _geneticService = new GeneticService(); 
            _tagService = new TagService();
        }


        public ZettelNoteNew ZettelNoteNew { get; set; } = new ZettelNoteNew();
        public ObservableCollection<NoteListLookUp> ZettelList { get; set; } = new ObservableCollection<NoteListLookUp>();
        public ObservableCollection<Shape> Figures { get; set; } = new ObservableCollection<Shape>();
        public int CanvasWidth { get; set; } = 800;
        public int CanvasHeight { get; set; } = 800;




        public bool IsVisibleTabZettelkastenNew { get; set; }

        public bool IsVisibleTabSearch { get; set; }

        public bool IsVisibleTabPlanning { get; set; }

        public bool IsVisibleTabZettelkasten { get; set; }

        public bool IsVisibleTabSelectionMap { get; set; }

        public bool IsVisibleTabNotepad { get; set; }
    }
}
