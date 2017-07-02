using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WheelOfLifeApp.Models
{
    public class ChangeCategoryViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Вы должны указать название категории")]
        public string NewCategoryName { get; set; }
        
        public string OldCategoryName { get; set; }

        public bool IsValid { get; set; }
    }
}