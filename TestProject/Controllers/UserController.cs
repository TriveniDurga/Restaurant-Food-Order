using System.Linq;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class UserController : Controller
    {
        AssignmentEntities context = new AssignmentEntities();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (IsUsernameExists(model.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exists. Choose a different username.");
                return View(model);
            }

            var user = new UserCredential { UserName = model.UserName, Password = model.Password };
            context.UserCredentials.Add(user);
            context.SaveChanges();

            TempData["Success"] = "Registration successful. Please sign in.";
            return RedirectToAction("Login", new { username = model.UserName });
        }

        public ActionResult Login(string username = null)
        {
            var model = new UserModel
            {
                UserName = username
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.UserName))
            {
                ModelState.AddModelError("", "Please enter username and password.");
                return View(model);
            }

            var user = context.UserCredentials.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

            if (user != null)
            {
                Session["Username"] = user.UserName;
                TempData["Success"] = $"Welcome, {user.UserName}!";
                return RedirectToAction("CreateOrder", "Restaurant");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }

        [HttpPost]
        public JsonResult IsUsernameAvailable(string username)
        {
            bool isUsernameAvailable = !context.UserCredentials.Any(u => u.UserName == username);
            Response.Cache.SetNoStore();
            return Json(isUsernameAvailable, JsonRequestBehavior.AllowGet);
        }

        private bool IsUsernameExists(string username)
        {
            return context.UserCredentials.Any(u => u.UserName == username);
        }
    }
}