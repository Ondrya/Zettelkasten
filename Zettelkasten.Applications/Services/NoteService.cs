using Zettelkasten.Applications.Interfaces;
using Zettelkasten.Domain.Models;

namespace Zettelkasten.Applications.Services
{
    /// <summary>
    /// Сервис для работы с заметками
    /// </summary>
    public class NoteService : INoteService
    {
        /// <summary>
        /// Провайдер данных
        /// </summary>
        private readonly IStorageService _storageService;

        public NoteService(IStorageService storageService)
        {
            //_storageService = new LocalFileStorageService();
            _storageService = storageService;
        }


        public int Create(Note note)
        {
            return _storageService.Create(note);
        }

        public List<Note> Get()
        {
            return _storageService.Get();
        }

        public Note Get(int id)
        {
            return _storageService.Get(id);
        }

        public void Update(Note note)
        {
            _storageService.Update(note);
        }

        public void Delete(int id)
        {
            _storageService.Delete(id);
        }
    }
}
