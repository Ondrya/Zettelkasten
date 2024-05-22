using System.Drawing;
using Zettelkasten.Applications.Interfaces;
using Zettelkasten.Domain.Models;
using Zettelkasten.Domain.Models.Painting;

namespace Zettelkasten.Applications.Services
{
    /// <summary>
    /// Сервис генетического алгоритма
    /// </summary>
    public class GeneticService : IGeneticService
    {
        private readonly Random _random;

        public GeneticService()
        {
            var randomize = Guid.NewGuid().GetHashCode();
            _random = new Random(randomize);
        }
                
        public List<List<PolarPointPolyColored>> MutateCollection(List<PolarPointPolyColored> points, int count)
        {
            var length = points.Count;
            var childs = new List<List<PolarPointPolyColored>>();
            for (int i = 0; i < count; i++)
            {
                foreach (var point in points)
                {
                    if (point.Colors == null)
                        point.Colors = new List<Color>();
                }
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

                    //swap angles
                    (newChild[indexA].AngleDeg, newChild[indexB].AngleDeg) =
                        (newChild[indexB].AngleDeg, newChild[indexA].AngleDeg);

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

        public List<PolarPointPolyColored> CreatePopulationFirst(Dictionary<string, List<int>> tagWithPoints, List<Note> notes)
        {
            var tagColors = tagWithPoints
                .Select(x => (x.Key, Color.FromArgb(
                    _random.Next(0, 255),
                    _random.Next(0, 255),
                    _random.Next(0, 255))))
                .ToDictionary(x => x.Key, x => x.Item2);

            // создаём точки

            var noteCount = notes.Count;
            var noteAngle = 360 / (double)noteCount;
            var radius = 150;
            double angle = 0.0;
            var points = new List<PolarPointPolyColored>();

            foreach (var note in notes)
            {
                var noteTags = note.Tags;
                if (noteTags.Count == 0)
                    note.Tags = new List<string>() { ConstantService.NoTagPlaceholder };
                var colors = tagColors.Where(kvp => note.Tags.Contains(kvp.Key)).Select(x => x.Value).ToList();
                var point = new PolarPointPolyColored(
                    radius, angle, colors, note.Id, $"{note.TagsLookUp()}---{note.Name}");
                points.Add(point);

                // prepare next step
                angle = angle + noteAngle;
            }

            return points;
        }

        public double CheckCollection(List<PolarPointPolyColored> points)
        {
            var colors = points
                .Select(x => x.Colors)
                .SelectMany(x => x)
                .Distinct()
                .ToList(); // список всех цветов
            var tagSectorDiffs = new List<(double, double)>();
            foreach (var color in colors)
            {
                var filteredPoints = points
                    .Where(x => x.Colors.Contains(color))
                    .OrderBy(x => x.AngleDeg)
                    .ToList();
                var min = filteredPoints.Min(x => x.AngleDeg);
                var max = filteredPoints.Max(x => x.AngleDeg);
                tagSectorDiffs.Add((min, max));
            }

            return tagSectorDiffs.Select(x => x.Item2 - x.Item1).Sum();
        }

        public List<List<PolarPointPolyColored>> Selection(List<PolarPointPolyColored> points, int childCount, int generationCount, int selectOnGenerartion)
        {
            var population = new List<List<PolarPointPolyColored>>() {
                points.DeepCopyList().ToList() };

            var step = 0;
            do
            {
                var nextPopulation = new List<List<PolarPointPolyColored>>();
                foreach (var item in population)
                {
                    var childrens = MutateCollection(item, childCount);
                    nextPopulation.AddRange(childrens);
                }

                population = nextPopulation;


                if (step % selectOnGenerartion == 0 && step > 0)
                {
                    population = FilterPopulation(childCount, population);
                }


                step = step + 1;
            }
            while (step < generationCount);

            return FilterPopulation(childCount, population);
        }

        public List<List<PolarPointPolyColored>> FilterPopulation(int childCount, List<List<PolarPointPolyColored>> population)
        {
            population = population
                .OrderBy(x => CheckCollection(x))
                .Take(childCount)
                .ToList();
            return population;
        }
    }
}
