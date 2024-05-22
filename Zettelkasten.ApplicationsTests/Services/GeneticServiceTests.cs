using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zettelkasten.Applications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zettelkasten.Applications.Interfaces;
using Zettelkasten.Domain.Models.Painting;

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

        [TestMethod()]
        public void GeneticServiceTest()
        {
            // просто инициализация
        }

        [TestMethod()]
        public void MutateCollectionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreatePopulationFirstTest()
        {
            var notes = _noteService.Get().Where(x => x != null);
            var tagCount = _tagService.GetTagsCount(notes);
            List<PolarPointPolyColored> points = _geneticService.CreatePopulationFirst(tagCount, notes.ToList());
            Assert.IsNotNull(points);   
        }

        [TestMethod()]
        public void CheckCollectionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SelectionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FilterPopulationTest()
        {
            Assert.Fail();
        }
    }
}