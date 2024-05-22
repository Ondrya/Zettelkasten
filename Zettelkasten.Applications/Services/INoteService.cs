using Zettelkasten.Domain.Models;

namespace Zettelkasten.Applications.Services
{
    public interface INoteService
    {
        int Create(Note note);
        void Delete(Note note);
        List<Note> Get();
        Note Get(int id);
        void Update(Note note);
    }
}