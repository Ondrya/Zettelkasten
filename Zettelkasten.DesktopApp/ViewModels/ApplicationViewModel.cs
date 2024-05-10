using Zettelkasten.Applications.Services;

namespace Zettelkasten.DesktopApp.ViewModels
{
    public partial class ApplicationViewModel : ViewModelBase
    {
        private readonly INoteService _noteService;


        public ApplicationViewModel()
        {
            ZettelNoteNew = new ZettelNoteNew();
            _noteService = new NoteService();
        }


        private ZettelNoteNew _zettelNoteNew;

        public ZettelNoteNew ZettelNoteNew
        {
            get => _zettelNoteNew;
            set 
            { 
                _zettelNoteNew = value; 
                OnPropertyChanged(nameof(ZettelNoteNew));
            }
        }
    }
}
