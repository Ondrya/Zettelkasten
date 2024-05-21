using Zettelkasten.Domain.Models;

namespace Zettelkasten.Applications.Interfaces
{
    /// <summary>
    /// Сервис ля работы с метками
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// Сгруппировать id заметок по тегам
        /// </summary>
        /// <param name="notes"></param>
        /// <returns></returns>
        Dictionary<string, List<int>> GetTagsCount(IEnumerable<Note> notes);
    }
}