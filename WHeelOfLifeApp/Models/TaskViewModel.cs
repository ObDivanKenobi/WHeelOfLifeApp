using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WheelOfLifeApp.Models
{
    public class TaskViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Название задачи не может быть пусто!")]
        public string Title { get; set; }

        public string Text { get; set; }

        public string RelatedCategory { get; set; }

        public bool IsValid { get; set; }

        public TaskViewModel()
        {
            IsValid = true;
        }
    }
}