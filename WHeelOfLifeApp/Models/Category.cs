using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WheelOfLifeApp.Models.Auth;

namespace WheelOfLifeApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}