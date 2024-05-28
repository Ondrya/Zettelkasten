using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zettelkasten.Domain.Models.Planning
{
    /// <summary>
    /// Элемент планирования
    /// </summary>
    public class Element
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public Group Group { get; set; }
    }
}
