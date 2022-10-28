using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkProject.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public List<BookTag> BookTags { get; set; }
    }
}
