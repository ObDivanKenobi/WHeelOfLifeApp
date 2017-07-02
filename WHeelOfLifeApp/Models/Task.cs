using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheelOfLifeApp.Models.Auth;

namespace WheelOfLifeApp.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public bool IsDone { get; set; }
        public virtual Category Category { get; set; }
    }
}