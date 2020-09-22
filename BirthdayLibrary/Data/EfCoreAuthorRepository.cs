using BirthdayLibrary.BLL.Models;
using BirthdayLibrary.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayLibrary.BLL.Data
{
    public class EfCoreAuthorRepository : EfCoreRepository<Book, ApplicationDbContext>
    {
        public EfCoreAuthorRepository(ApplicationDbContext context) : base(context)
        {

        }
        // We can add new methods specific to the movie repository here in the future
    }
}
