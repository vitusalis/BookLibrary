using BirthdayLibrary.BLL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthdayLibrary.BLL.Models
{
    public class BookAuthor 
    {
        [Key]
        [Column(Order=1)]
        public int BookId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int AuthorId { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }
        
    }
}
