using Newtonsoft.Json;
using Zettelkasten.Applications.Interfaces;
using Zettelkasten.Domain.Models;

namespace Zettelkasten.Applications.Services
{
    public class FakeStorageService : IStorageService
    {
        public int Create(Note note)
        {
            throw new NotImplementedException();
        }

        public void Delete(Note note)
        {
            throw new NotImplementedException();
        }

        public List<Note> Get()
        {
            var json = $@"[
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 1,
    ""Name"": ""новая идея..."",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T08:11:48.4633217+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 10,
    ""Name"": ""новая идея..."",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T09:49:27.2274512+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 11,
    ""Name"": ""новая идея...25"",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T11:56:42.1584324+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [
      ""aftercommit""
    ],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 12,
    ""Name"": ""новая идея...100500"",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T15:19:51.1267836+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [
      ""обучение""
    ],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 13,
    ""Name"": ""Валидация"",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-21T20:16:43.9792243+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [
      ""test""
    ],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 2,
    ""Name"": ""новая идея..."",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T08:12:38.7104931+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [
      ""test""
    ],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 3,
    ""Name"": ""новая идея..."",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T08:12:46.9200429+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [
      ""test2""
    ],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 4,
    ""Name"": ""новая идея..."",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T08:12:52.8455609+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [
      ""test2""
    ],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 5,
    ""Name"": ""новая идея..."",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T08:12:59.7101089+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [
      ""test3""
    ],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 6,
    ""Name"": ""новая идея..."",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T08:13:06.8551788+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [
      ""test3"",
      ""test2""
    ],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 7,
    ""Name"": ""новая идея..."",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T08:13:13.5895179+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [
      ""test""
    ],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 8,
    ""Name"": ""tada"",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T08:41:29.9472571+03:00""
  }},
  {{
    ""Content"": ""заполни меня..."",
    ""ParentNoteId"": 0,
    ""Tags"": [],
    ""NoteConnections"": null,
    ""Attachments"": null,
    ""Id"": 9,
    ""Name"": ""новая идея..."",
    ""Description"": null,
    ""CreatedAt"": ""2024-05-14T08:42:57.4532585+03:00""
  }}
]";
            return JsonConvert.DeserializeObject<List<Note>>(json);
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
