using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Zettelkasten.Domain.Models;

namespace Zettelkasten.Applications.Services
{
    public class NoteService
    {
        private readonly IStorageService _storageService;

        public NoteService()
        {
            _storageService = new LocalFileStorageService();
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
            throw new NotImplementedException();
        }

        public void Update(Note note)
        {
            throw new NotImplementedException();
        }

        public void Delete(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
