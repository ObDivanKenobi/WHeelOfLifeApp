using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WheelOfLifeApp.Models
{
    public class ChangeTaskViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Название задачи не может быть пусто!")]
        public string NewTitle { get; set; }

        public string OldTitle { get; set; }

        public string Text { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public string RelatedCategory { get; set; }

        public string NewCategory { get; set; }

        public bool IsValid { get; set; }
    }
}