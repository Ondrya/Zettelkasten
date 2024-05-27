using Zettelkasten.Applications.Interfaces;
using Zettelkasten.Domain.Models.Painting;
using Newtonsoft.Json;
using System.Drawing;

namespace Zettelkasten.Applications.Services.Tests
{
    [TestClass()]
    public class GeneticServiceTests
    {
        private IStorageService _storageService;
        private NoteService _noteService;
        private GeneticService _geneticService;
        private TagService _tagService;

        [TestInitialize]
        public void Init()
        {
            _storageService = new FakeStorageService();
            _noteService = new NoteService(_storageService);
            _geneticService = new GeneticService();
            _tagService = new TagService();
        }

        [TestMethod("Инициализация сервиса")]
        public void GeneticServiceTest()
        {
            // просто инициализация
        }

        [TestMethod("Потомок отличаеится от родителя")]
        public void MutateCollectionTest()
        {
            var firstPopulation = CreatePopulationFirstInner();
            var firstJson = JsonConvert.SerializeObject(firstPopulation);

            var newInstanse = JsonConvert.DeserializeObject<List<PolarPointPolyColored>>(firstJson);
            var nextPopulation = _geneticService.MutateCollection(newInstanse, 1)[0];
            var nextJson = JsonConvert.SerializeObject(nextPopulation);

            var firstAngles = firstPopulation.Select(x => x.AngleDeg);
            var nextAngles = nextPopulation.Select(x => x.AngleDeg);

            Assert.AreNotEqual(firstAngles, nextAngles);
        }

        [TestMethod("Создаётся предок для всех")]
        public void CreatePopulationFirstTest()
        {
            var firstPopulation = CreatePopulationFirstInner();
            Assert.IsNotNull(firstPopulation);
        }

        /// <summary>
        /// Вызов создания предка.
        /// </summary>
        /// <returns></returns>
        private List<PolarPointPolyColored> CreatePopulationFirstInner()
        {
            var notes = _noteService.Get().Where(x => x != null);
            var tagCount = _tagService.GetTagsCount(notes);
            List<PolarPointPolyColored> points = _geneticService.CreatePopulationFirst(tagCount, notes.ToList());
            return points;
        }

        [TestMethod("Провалидировать коллекцию")]
        public void CheckCollectionTest()
        {
            var firstPopulation = CreatePopulationFirstInner();
            
            var colorCount = new Dictionary<Color, int>();
            foreach (var item in firstPopulation)
            {
                foreach (var color in item.Colors)
                {
                    if (colorCount.ContainsKey(color))
                    {
                        colorCount[color]++;
                    }
                    else
                    {
                        colorCount.Add(color, 1);
                    }
                }
            }

            var hasColorAtLeastTwice = colorCount.Any(x => x.Value > 1);
            if (!hasColorAtLeastTwice)
                return;

            var propbe = _geneticService.CheckCollection(firstPopulation);
            Assert.IsTrue(propbe>0);
        }

        [TestMethod("Проверка селекции - каждая следующая проба должна быть не хуже первой")]
        public void SelectionTest()
        {
            var population = CreatePopulationFirstInner();
            var probFirst = _geneticService.CheckCollection(population);
            var selection = _geneticService.Selection(population, 4, 5, 5);
            foreach (var item in selection)
            {
                var prob = _geneticService.CheckCollection(item);
                var comapreResult = probFirst >= prob;
                var comapreResultString = comapreResult ? "GREATER or EQUAL" : "LESS !!!";
                Console.WriteLine($"{probFirst} is {comapreResultString} then {prob}");
                Assert.IsTrue(comapreResult);
            }
        }

        [TestMethod("Отфильтрованная по пробе коллекция содержит лучшие значения")]
        public void FilterPopulationTest()
        {
            var count = 2;
            var firstPopulation = CreatePopulationFirstInner();
            var nextGeneration = _geneticService.MutateCollection(firstPopulation, 100);
            var filterResult = _geneticService
                .FilterPopulation(count, nextGeneration)
                .Select(x => _geneticService.CheckCollection(x))
                .ToList();
            var probMaxMin = nextGeneration
                .Select(x => _geneticService.CheckCollection(x))
                .OrderBy(x => x)
                .Take(count)
                .Max();

            Assert.IsTrue(filterResult.Count == count);
            Assert.IsTrue(!filterResult.Exists(x => x > probMaxMin));
        }
    }
}