using Zettelkasten.Domain.Models;
using Zettelkasten.Domain.Models.Painting;

namespace Zettelkasten.Applications.Interfaces
{
    /// <summary>
    /// Сервис генетического алгоритма
    /// </summary>
    public interface IGeneticService
    {
        /// <summary>
        /// Оценка популяции для отбора
        /// </summary>
        /// <param name="points">коллекция полярных координат</param>
        /// <returns></returns>
        double CheckCollection(List<PolarPointPolyColored> points);

        /// <summary>
        /// Создание первой популяции
        /// </summary>
        /// <param name="tagWithPoints">теги с id записей</param>
        /// <param name="notes">записи</param>
        /// <returns></returns>
        List<PolarPointPolyColored> CreatePopulationFirst(Dictionary<string, List<int>> tagWithPoints, List<Note> notes);

        /// <summary>
        /// Произвести отбор потомков по критерию отбора
        /// </summary>
        /// <param name="childCount">сколько потомков оставить</param>
        /// <param name="population">популяция для отбора</param>
        /// <returns></returns>
        List<List<PolarPointPolyColored>> FilterPopulation(int childCount, List<List<PolarPointPolyColored>> population);

        /// <summary>
        /// Мутировать коллекцию - создать поколение потомков
        /// </summary>
        /// <param name="points">коллекция - родитель</param>
        /// <param name="count">кол-во потомков</param>
        /// <returns></returns>
        List<List<PolarPointPolyColored>> MutateCollection(List<PolarPointPolyColored> points, int count);

        /// <summary>
        /// Произвести селекцию
        /// </summary>
        /// <param name="points">первичная популяция</param>
        /// <param name="childCount">кол-во потомков в каждом следующем поколении</param>
        /// <param name="generationCount">кол-во поколений</param>
        /// <param name="selectOnGenerartion">произвести отбор на поколении кратном этому числу</param>
        /// <returns>коллекция популяций</returns>
        List<List<PolarPointPolyColored>> Selection(List<PolarPointPolyColored> points, int childCount, int generationCount, int selectOnGenerartion);
    }
}