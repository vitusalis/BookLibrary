using BirthdayLibrary.BLL.Models.Base;
using System;
using System.Collections.Generic;

namespace BirthdayLibrary.BLL.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int YearReleased { get; set; }
        public int[] AuthorIds { get; set; }
    }
}
