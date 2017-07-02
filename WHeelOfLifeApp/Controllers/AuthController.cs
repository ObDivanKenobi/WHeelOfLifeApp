using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using WheelOfLifeApp.Models.Auth;
using System.Web;
using System.Web.Mvc;
using WheelOfLifeApp.Models;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Web.Configuration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WheelOfLifeApp.Controllers
{
    public class AuthController : Controller
    {
        ApplicationDbContext context;

        private IAuthenticationManager AuthManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

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

        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(Login details, string returnUrl)
        {
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
            UserStore Store = new UserStore(new ApplicationDbContext());
            UserManager userManager = new UserManager(Store);
            ApplicationUser user = await userManager.FindAsync(details.EmailOrUsername, details.Password);
            if (user == null)
            {
                user = await userManager.FindByEmailAsync(details.EmailOrUsername);
                if (user != null)
                    user = await userManager.CheckPasswordAsync(user, details.Password) ? user : null;
            }

            if (user == null)
                ModelState.AddModelError("", "Неверно введено имя пользователя или пароль.");
            else
            {
                ClaimsIdentity ident = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
                return Redirect(returnUrl);
            }

            return View(details);                
        }

        public ActionResult LogOut(string returnUrl)
        {
            AuthManager.SignOut();
            return Redirect(returnUrl);
            //return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    string defaultRoleName = WebConfigurationManager.AppSettings["defaultUserRole"];
                    IdentityResult iresult = await UserManager.AddToRoleAsync(user.Id, defaultRoleName);
                    if (iresult.Succeeded)
                        return RedirectToAction("Index", "Home", null);
                    else
                        return View("Error", new string[] { "Ошибка регистрации." });
                }
                else
                    AddErrorsFormResult(result);
            }
            return View(model);
        }

        private void AddErrorsFormResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
                ModelState.AddModelError("", error);
        }
    }
}