using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkProject.Models
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public List<int> TagIds { get; set; }
        public List <BookTag> BookTags { get; set; }
    }
}
