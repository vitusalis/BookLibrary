using BirthdayLibrary.BLL.Models.Base;
using System;
using System.Collections.Generic;


namespace BirthdayLibrary.BLL.Models
{
    public class BookAuthor : IEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        
    }
}
