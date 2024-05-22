using Zettelkasten.Domain.Models;

namespace Zettelkasten.Applications.Interfaces
{
    /// <summary>
    /// Провайдер хранилища данных
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        int Create(Note note);

        /// <summary>
        /// Удаление щаписи
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns></returns>
        List<Note> Get();

        Note Get(int id);

        void Update(Note note);
    }
}