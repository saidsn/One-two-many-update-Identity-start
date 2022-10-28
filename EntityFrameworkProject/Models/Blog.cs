using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkProject.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "SignPhoto can't be empty")]
        public IFormFile Photo { get; set; }
        public DateTime Date { get; set; }
    }
}
