﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zettelkasten.Domain.Models;

namespace Zettelkasten.Applications.Services
{
    public class TagService
    {
        public Dictionary<string, List<int>> GetTagsCount(IEnumerable<Note> notes)
        {
            var noteTags = notes.Select(x => new { x.Id, x.Tags }).ToList();
            var tags = new Dictionary<string, List<int>>();

            tags.Add(ConstantService.NoTagPlaceholder, new List<int>());

            foreach (var noteTag in noteTags)
            {
                var keys = noteTag.Tags;
                if (keys == null)
                {
                    tags[ConstantService.NoTagPlaceholder].Add(noteTag.Id);
                }
                else
                {
                    foreach (var item in keys)
                    {
                        if (tags.ContainsKey(item))
                        {
                            tags[item].Add(noteTag.Id);
                        }
                        else
                        {
                            tags.Add(item, new List<int>() { noteTag.Id });
                        }
                    }
                }
            }

            return tags;
        }
    }
}
