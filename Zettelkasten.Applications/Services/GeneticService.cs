using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zettelkasten.Domain.Models;
using Zettelkasten.Domain.Models.Painting;

namespace Zettelkasten.Applications.Services
{
    public class GeneticService
    {
        public List<List<PolarPointPolyColored>> MutateCollection(List<PolarPointPolyColored> points, int count)
        {
            // todo сделать 4 вариации, поменяв местами 2 точки
            // todo повторить 10 раз
            // todo убить всех потомков кроме 4 с лучшей апроксимацией, сохранив первоначального родителя
        }


        public List<PolarPointPolyColored> CreatePopulationFirst(Dictionary<string, List<int>> tagCount, List<Note> notes)
        {
            Random random = new Random();
            var tagColors = tagCount.Select(x => (x.Key, Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)))).ToDictionary(x => x.Key, x => x.Item2);

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
        /// <returns>коллекция популяций</returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<List<PolarPointPolyColored>> Selection(List<PolarPointPolyColored> points, int childCount, int generationCount)
        {
            var population = new List<List<PolarPointPolyColored>>() { points };
            do
            {
                var nextPopulation = new List<List<PolarPointPolyColored>>();
                foreach (var item in population)
                {
                    var childrens = MutateCollection(points, childCount);
                    nextPopulation.AddRange(childrens);
                }
                
                population = nextPopulation;

                generationCount -= 1;
            }
            while (generationCount > 0);
            
            return population;
        }
    }
}
