using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WheelOfLifeApp.Models.Auth;

namespace WheelOfLifeApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("defaultConnectionString") { }

        public virtual DbSet<Category> CategorySet { get; set; }
        public virtual DbSet<Task> TaskSet { get; set; }
    }
}