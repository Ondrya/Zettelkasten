using Newtonsoft.Json;
using Zettelkasten.Applications.Interfaces;
using Zettelkasten.Domain.Models;

namespace Zettelkasten.Applications.Services
{
    /// <summary>
    /// Хранилище на основе файлов
    /// </summary>
    public class LocalFileStorageService : IStorageService
    {
        private readonly List<string> storage;
        private readonly string delimiter = "---";
        private readonly string storageFolderName = "AppData";

        public LocalFileStorageService()
        {
            storage = new List<string>();
            var files = GetAllFileInStorage();
            if (files.Count > 0)
                storage.AddRange(files);
        }

        /// <summary>
        /// Получить путь до текущего файлового хранилища
        /// </summary>
        /// <returns></returns>
        private string GetCurrentStoragePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + @$"\{storageFolderName}";
        }


        /// <summary>
        /// Список всех файлов в хранилище
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllFileInStorage()
        {
            return Directory
                .GetFiles(GetCurrentStoragePath())
                .Select(x => Path.GetFileNameWithoutExtension(x))
                .Where(x => x != "0")
                .ToList();
        }


        public int Create(Note note)
        {
            note.Id = GetNextId();
            var filename = $"{note.Id}.txt";
            var path = Path.Combine(GetCurrentStoragePath(), filename);
            File.WriteAllText(path, JsonConvert.SerializeObject(note));
            storage.Add(note.Id.ToString());
            return note.Id;
        }


        public List<Note> Get()
        {
            var basePath = GetCurrentStoragePath();
            var notes = new List<Note>();

            foreach (var item in storage)
            {
                var path = Path.Combine(basePath, $"{item}.txt");
                var fileContent = File.ReadAllText(path);
                var note = JsonConvert.DeserializeObject<Note>(fileContent);
                if (note.Tags == null)
                    note.Tags = new List<string>();
                notes.Add(note);
            }

            return notes;
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


        private int GetNextId()
        {
            if (storage.Count == 0)
                return 1;
            return storage.Max(x => int.Parse(x.Split(delimiter)[0])) + 1;
        }
    }
}
