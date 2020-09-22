using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BirthdayLibrary.BLL.Models.Base
{
    public interface IEntity
    {
        int Id { get; set; }
    }
}
