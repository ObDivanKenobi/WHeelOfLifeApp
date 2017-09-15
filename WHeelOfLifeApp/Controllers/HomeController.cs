using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using WheelOfLifeApp.Models;
using WheelOfLifeApp.Models.Auth;

namespace WheelOfLifeApp.Controllers
{
    public class HomeController : Controller
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTask(string categoryName)
        {
            return PartialView(new TaskViewModel { Title = "", Text = "", RelatedCategory = categoryName, IsValid = false });
        }

        [HttpPost]
        public ActionResult AddTask(TaskViewModel newTask)
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            newTask.IsValid = ModelState.IsValid;
            Category c = user.Categories.FirstOrDefault(x => x.Name == newTask.RelatedCategory);
            if (c == null)
            {
                ModelState.AddModelError("", "Категории, в которой вы пытались создать задачу, нет. Как вы это сделали?");
                newTask.IsValid = false;
            }
            if (c.Tasks.Where(x => x.Title == newTask.Title).Count() != 0)
            {
                ModelState.AddModelError("", "В этой категории уже есть задача с таким названием!");
                newTask.IsValid = false;
            }

            if (newTask.IsValid)
            {
                Models.Task task = new Models.Task();
                task.Title = newTask.Title;
                task.Text = newTask.Text;
                task.Category = c;
                c.Tasks.Add(task);
                DbContext.TaskSet.Add(task);
                DbContext.SaveChanges();
            }

            return PartialView(newTask);
        }

        public ActionResult RemoveTask(string categoryName, string taskTitle)
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            var category = user.Categories.FirstOrDefault(x => x.Name == categoryName);
            if (category != null)
            {
                var taskToDelete = category.Tasks.Where(x => x.Title == taskTitle).FirstOrDefault();
                if (taskToDelete != null)
                {
                    category.Tasks.Remove(taskToDelete);
                    DbContext.TaskSet.Remove(taskToDelete);
                    DbContext.SaveChanges();
                }
            }

            return RedirectToAction("GetTasks");
        }

        [HttpGet]
        public ActionResult ChangeTask(string categoryName, string taskTitle)
        {
            if (string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrWhiteSpace(taskTitle))
                return PartialView(new ChangeTaskViewModel { IsValid = false });

            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            var category = user.Categories.FirstOrDefault(x => x.Name == categoryName);
            var taskToUpdate = category.Tasks.Where(x => x.Title == taskTitle).FirstOrDefault();

            ChangeTaskViewModel model = new ChangeTaskViewModel { OldTitle = taskTitle, IsValid = false, RelatedCategory = categoryName, Text = taskToUpdate.Text };
            if (category == null)
                ModelState.AddModelError("", "Категория не найдена. Как вы это сделали?");
            else
                model.RelatedCategory = category.Name;
            if (taskToUpdate == null)
                ModelState.AddModelError("", "Категория не найдена. Как вы это сделали?");
            else
                model.Text = taskToUpdate.Text;

            List<SelectListItem> categories = new List<SelectListItem>();
            var tmp = user.Categories.ToList();
            for(int i = 0; i < user.Categories.Count; ++i)
            {
                if (tmp[i].Name == categoryName)
                    categories.Add(new SelectListItem { Text = tmp[i].Name, Value = tmp[i].Name, Selected = true });
                else
                    categories.Add(new SelectListItem { Text = tmp[i].Name, Value = tmp[i].Name });
            }
            model.Categories = categories;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ChangeTask(ChangeTaskViewModel updatedTask)
        {
            updatedTask.IsValid = ModelState.IsValid;

            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            var category = user.Categories.FirstOrDefault(x => x.Name == updatedTask.RelatedCategory);

            if ((updatedTask.OldTitle != updatedTask.NewTitle) && category.Tasks.Where(x => x.Title == updatedTask.NewTitle).Count() != 0)
            {
                ModelState.AddModelError("", "Задача с таким названием уже есть");

                List<SelectListItem> categories = new List<SelectListItem>();
                var tmp = user.Categories.ToList();
                for (int i = 0; i < user.Categories.Count; ++i)
                {
                    if (tmp[i].Name == updatedTask.RelatedCategory)
                        categories.Add(new SelectListItem { Text = tmp[i].Name, Value = tmp[i].Name, Selected = true });
                    else
                        categories.Add(new SelectListItem { Text = tmp[i].Name, Value = tmp[i].Name });
                }
                updatedTask.Categories = categories;

                updatedTask.IsValid = false;
            }

            if (ModelState.IsValid)
            {
                var taskToUpdate = category.Tasks.Where(x => x.Title == updatedTask.OldTitle).FirstOrDefault();
                if (updatedTask.NewCategory != category.Name)
                {
                    category.Tasks.Remove(taskToUpdate);
                    category = user.Categories.FirstOrDefault(x => x.Name == updatedTask.NewCategory);
                    category.Tasks.Add(taskToUpdate);
                }

                taskToUpdate.Title = updatedTask.NewTitle;
                taskToUpdate.Text = updatedTask.Text;
                DbContext.SaveChanges();
                updatedTask.IsValid = true;
            }

            return PartialView(updatedTask);
        }

        public ActionResult UpdateTaskStatus(string categoryName, string taskTitle, bool isDone)
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            var category = user.Categories.FirstOrDefault(x => x.Name == categoryName);
            var taskToUpdate = category.Tasks.Where(x => x.Title == taskTitle).FirstOrDefault();

            taskToUpdate.IsDone = isDone;
            DbContext.SaveChanges();

            return RedirectToAction("GetTasks");
        }

        public ActionResult AddCategory()
        {
            return PartialView(new CategoryViewModel { IsValid = false });
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel newCat)
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            newCat.IsValid = ModelState.IsValid;

            if (user.Categories.Where(x => x.Name == newCat.CategoryName).Count() != 0)
            {
                ModelState.AddModelError("", "Такая категория уже есть!");
                newCat.IsValid = false;
            }
            
            if(ModelState.IsValid)
            {
                Category c = new Category();
                c.Name = newCat.CategoryName;
                c.Owner = user;
                user.Categories.Add(c);
                DbContext.CategorySet.Add(c);
                DbContext.SaveChanges();
            } 
            
            return PartialView(newCat);          
        }

        [HttpGet]
        public ActionResult ChangeCategory(string categoryName)
        {
            if(string.IsNullOrWhiteSpace(categoryName))
                return PartialView(new ChangeCategoryViewModel { IsValid = false });

            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            var categoryToUpdate = user.Categories.FirstOrDefault(x => x.Name == categoryName);
            ChangeCategoryViewModel model = new ChangeCategoryViewModel();
            model.OldCategoryName = categoryName;
            if (categoryToUpdate == null)
            {
                ModelState.AddModelError("", "Категория не найдена. Как вы это сделали?");
            }

            model.IsValid = false;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ChangeCategory(ChangeCategoryViewModel updatedCat)
        {
            updatedCat.IsValid = ModelState.IsValid;
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);

            if (user.Categories.Where(x => x.Name == updatedCat.NewCategoryName).Count() != 0)
            {
                ModelState.AddModelError("", "Такая категория уже есть!");
                updatedCat.IsValid = false;
            }

            if (ModelState.IsValid)
            {
                Category categoryToUpdate = user.Categories.FirstOrDefault(x => x.Name == updatedCat.OldCategoryName);
                categoryToUpdate.Name = updatedCat.NewCategoryName;
                DbContext.SaveChanges();
                updatedCat.IsValid = true;
            }

            return PartialView(updatedCat);
        }

        public ActionResult RemoveCategory(string categoryName)
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            var categoryToDelete = user.Categories.Where(x => x.Name == categoryName).FirstOrDefault();
            if (categoryToDelete != null)
            {
                user.Categories.Remove(categoryToDelete);
                DbContext.CategorySet.Remove(categoryToDelete);
                DbContext.SaveChanges();
            }

            return RedirectToAction("GetTasks");
        }

        public ActionResult GetTasks()
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            Dictionary<string, List<Models.Task>> tasks = new Dictionary<string, List<Models.Task>>();
            var categories = user.Categories.ToList();

            foreach (Category c in categories)
            {
                List<Models.Task> relatedTasks = c.Tasks.ToList();
                tasks.Add(c.Name, relatedTasks);
            }

            return PartialView(tasks);
        }
        
        public JsonResult GetData()
        {
            Random r = new Random();
            List<CategoryForJson> list = new List<CategoryForJson>();
            int countTasks = DbContext.TaskSet.Count();
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);

            var categories = DbContext.CategorySet.Where(x => x.Owner.Id == user.Id).ToList();

            foreach (Category c in categories)
            {
                int cntCategoryTasks = c.Tasks.Count;
                int doneTasks = c.Tasks.Where(x => x.IsDone).Count();
                double percent = cntCategoryTasks != 0 ? ((double)doneTasks / cntCategoryTasks) : 0;
                double correctedPercent = 10 + percent*90;
                list.Add(new CategoryForJson(c.Name+c.Id.ToString(), cntCategoryTasks, doneTasks, correctedPercent, 0.5 + cntCategoryTasks, ColorTranslator.ToHtml(Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0,255))), c.Name));
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}