﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Zettelkasten.Domain.Models
{
    /// <summary>
    /// Запись
    /// </summary>
    public class Note : ModelBase
    {
        /// <summary>
        /// Текст записи
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Родительская запись
        /// </summary>
        public int ParentNoteId { get; set; }

        /// <summary>
        /// Теги
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Связи с другими записями
        /// </summary>
        public List<NoteConnection> NoteConnections { get; set; }

        public List<AttachmentNote> Attachments { get; set; }

        public string TagsLookUp()
        {
            return string.Join("", Tags.Select(x => $"#{x};"));
        }

        public NoteListLookUp ToListLookUp()
        {
            var item = new NoteListLookUp();
            item.Id = this.Id;
            item.Name = this.Name;
            item.Description = this.Description;
            item.CreatedAt = this.CreatedAt;
            item.Tags = this.Tags != null ? string.Join(", ", this.Tags) : "";
            item.ParentNoteId = this.ParentNoteId;


            return item;
        }
    }
}
