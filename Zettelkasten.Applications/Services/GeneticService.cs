using System.Drawing;
using Zettelkasten.Domain.Models;
using Zettelkasten.Domain.Models.Painting;

namespace Zettelkasten.Applications.Services
{
    public class GeneticService
    {
        private readonly Random _random;

        public GeneticService()
        {
            _random = new Random();
        }


        /// <summary>
        /// Мутировать коллекцию - создать поколение потомков
        /// </summary>
        /// <param name="points">коллекция - родитель</param>
        /// <param name="count">кол-во потомков</param>
        /// <returns></returns>
        public List<List<PolarPointPolyColored>> MutateCollection(List<PolarPointPolyColored> points, int count)
        {
            var length = points.Count;
            var childs = new List<List<PolarPointPolyColored>>();
            for (int i = 0; i < count; i++)
            {
                // var newChild = ExtensionService.DeepCopy<List<PolarPointPolyColored>>(points);
                var newChild = points.DeepCopyList().ToList();
                if (count > 1)
                {
                    var indexA = _random.Next(length);
                    int indexB;
                    do
                    {
                        indexB = _random.Next(length);
                    }
                    while (indexA == indexB);
                    newChild.Swap(indexA, indexB);
                    childs.Add(newChild);
                }
                else
                {
                    childs.Add(newChild);
                    break;
                }
            }
            return childs;
        }


        public List<PolarPointPolyColored> CreatePopulationFirst(Dictionary<string, List<int>> tagCount, List<Note> notes)
        {
            var tagColors = tagCount.Select(x => (x.Key, Color.FromArgb(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255)))).ToDictionary(x => x.Key, x => x.Item2);

            // создаём точки

            var noteCount = notes.Count;
            var noteAngle = 360 / (double)noteCount;
            var radius = 200;
            double angle = 0.0;
            var points = new List<PolarPointPolyColored>();

            foreach (var note in notes)
            {

                var noteTags = note.Tags;
                if (noteTags.Count == 0)
                    note.Tags = new List<string>() { ConstantService.NoTagPlaceholder };
                var colors = tagColors.Where(kvp => note.Tags.Contains(kvp.Key)).Select(x => x.Value).ToList();
                var point = new PolarPointPolyColored(radius, angle, colors, note.Id, note.Name);
                points.Add(point);

                // prepare next step
                angle = angle + noteAngle;
            }

            return points;
        }


        /// <summary>
        /// Проба дляя генетического алгоритма
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public double CheckCollection(List<PolarPointPolyColored> points)
        {
            var colors = points.Select(x => x.Colors).SelectMany(x => x).Distinct().ToList(); // список всех цветов
            var tagSectorDiffs = new List<(double, double)>();
            foreach (var color in colors)
            {
                var filteredPoints = points.Where(x => x.Colors.Contains(color)).OrderBy(x => x.AngleDeg).ToList();
                var min = filteredPoints.Min(x => x.AngleDeg);
                var max = filteredPoints.Max(x => x.AngleDeg);
                tagSectorDiffs.Add((min, max));
            }

            return tagSectorDiffs.Select(x => x.Item2 - x.Item1).Sum();
        }


        /// <summary>
        /// Произвести селекцию
        /// </summary>
        /// <param name="points">первичная популяция</param>
        /// <param name="childCount">кол-во потомков в каждом следующем поколении</param>
        /// <param name="generationCount">кол-во поколений</param>
        /// <param name="selectOnGenerartion">произвести отбор на поколении кратном этому числу</param>
        /// <returns>коллекция популяций</returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<List<PolarPointPolyColored>> Selection(List<PolarPointPolyColored> points, int childCount, int generationCount, int selectOnGenerartion)
        {
            var population = new List<List<PolarPointPolyColored>>() { points };
            var step = 0;
            do
            {
                var nextPopulation = new List<List<PolarPointPolyColored>>();
                foreach (var item in population)
                {
                    var childrens = MutateCollection(points, childCount);
                    nextPopulation.AddRange(childrens);
                }
                
                population = nextPopulation;


                if (step % selectOnGenerartion == 0)
                {
                    population = FilterPopulation(childCount, population);
                }


                step = step + 1;
            }
            while (step < generationCount);
            
            return FilterPopulation(childCount, population);
        }


        private List<List<PolarPointPolyColored>> FilterPopulation(int childCount, List<List<PolarPointPolyColored>> population)
        {
            population = population.OrderBy(x => CheckCollection(x)).Take(childCount).ToList();
            return population;
        }
    }
}
