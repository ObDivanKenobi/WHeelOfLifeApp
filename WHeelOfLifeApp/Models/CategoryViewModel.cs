using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WheelOfLifeApp.Models
{
    public class CategoryViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Вы должны указать название категории")]
        public string CategoryName { get; set; }

        public bool IsValid { get; set; }
    }
}