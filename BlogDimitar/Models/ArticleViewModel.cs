using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BlogDimitar.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Текст")]
        public string Content { get; set; }

        public string AuthorId { get; set; }
    }
}