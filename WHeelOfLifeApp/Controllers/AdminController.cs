using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WheelOfLifeApp.Models;
using WheelOfLifeApp.Models.Admin;
using WheelOfLifeApp.Models.Auth;
using WHeelOfLifeApp.Models.Auth;

namespace WheelOfLifeApp.Controllers
{
    [ExtendedAuthorize(Roles = "Администратор", RedirectUrl = "/Errors/UnauthorizedRequest")]
    public class AdminController : Controller
    {
        ApplicationDbContext context;

        private ApplicationDbContext DbContext
        {
            get
            {
                if (context == null) context = new ApplicationDbContext();
                return context;
            }
        }

        private UserManager UserManager
        {
            get { return new UserManager(new UserStore<ApplicationUser>(DbContext)); }
        }

        private RoleManager<IdentityRole> RoleManager
        {
            get { return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(DbContext)); }
        }
        
        
        // GET: Admin
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ViewBag.Users = context.Users.ToList();
            ViewBag.Roles = context.Roles.ToList();
            return View();
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole(CreateRoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new IdentityRole(role.RoleName));

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    AddErrorsFromResult(result);
            }
            return View(role);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRole(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    return View("Error", result.Errors);
            }
            else
                return View("Error", new string[] { "Роль не найдена" });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    return View("Error", result.Errors);
            }
            else
                return View("Error", new string[] { "Пользователь не найден" });
        }

        public async Task<ActionResult> EditRole(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);
            string[] memberIds = role.Users.Select(x => x.UserId).ToArray();

            IEnumerable<ApplicationUser> members = UserManager.Users.Where(x => memberIds.Any(y => y == x.Id));
            IEnumerable<ApplicationUser> nonMembers = UserManager.Users.Except(members);

            return View(new RoleEditModel { Role = role, Members = members, NonMembers = nonMembers });
        }

        [HttpPost]
        public async Task<ActionResult> EditRole(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    result = await UserManager.AddToRoleAsync(userId, model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    result = await UserManager.RemoveFromRoleAsync(userId,
                    model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                return RedirectToAction("Index");

            }
            return View("Error", new string[] { "Роль не найдена" });
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}