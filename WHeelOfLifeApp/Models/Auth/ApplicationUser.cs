using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WheelOfLifeApp.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Category> Categories { get; set; }
    }
}