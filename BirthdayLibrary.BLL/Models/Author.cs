using BirthdayLibrary.BLL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BirthdayLibrary.BLL.Models
{
    public class Author : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
