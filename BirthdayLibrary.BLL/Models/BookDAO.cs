﻿using BirthdayLibrary.BLL.Models.Base;
using System;
using System.Collections.Generic;

namespace BirthdayLibrary.BLL.Models
{
    public class BookDAO 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int YearReleased { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}
