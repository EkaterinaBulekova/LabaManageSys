using System.Web.Mvc;
using System.Web.Security;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.Account;

namespace LabaManageSys.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUsersRepository repository;
        private ILogger log;
        private string defaultRole = "Students";

        public AccountController(IUsersRepository repo, ILogger logg)
        {
            this.repository = repo;
            this.log = logg;
        }

        public ActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.View();
            }

            this.TempData["message"] = string.Format("Вам необходимо сначала выйти из логина: {0}", User.Identity.Name);
            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.repository.GetUserByName(model.Name);

                if (user != null)
                {
                    if (this.repository.UserPasswordValidate(user, model.Password))//if c тройной вложенностью не очень удачное решение.
                                                                                   //лучше этого избегать. Можно совмещать условия в одном if или выносить в отдельные методы 
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, false);
                        this.log.Info("User " + model.Name + " logged in");
                        return this.RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        this.TempData["message"] = string.Format("Пользователя с таким логином и паролем нет");
                    }
                }
                else
                {
                    this.TempData["message"] = string.Format("Пользователя с таким логином нет");
                }
            }

            return this.View(model);
        }

        public ActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.View();
            }

            this.TempData["message"] = string.Format("Вам необходимо сначала выйти из логина: {0}", User.Identity.Name);
            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.repository.GetUserByName(model.Name) == null && this.repository.GetUserByName(model.Email) == null)
                {
                    var newUser = new UserModel
                    {
                        UserId = 0,
                        Name = model.Name,
                        Email = model.Email,
                        RoleId = this.repository.GetRoleByName(this.defaultRole).RoleId
                    };
                    this.repository.UserUpdate(newUser);
                    this.repository.UserPasswordSet(newUser, model.Password);

                    FormsAuthentication.SetAuthCookie(model.Name, false);
                    this.log.Info("User " + model.Name + " created");
                    this.log.Info("User " + model.Name + " logged in");
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    this.TempData["message"] = string.Format("Пользователь с таким логином уже существует");
                }
            }

            return this.View(model);
        }

        [Authorize]
        public ActionResult Logoff()
        {
            if (User.Identity.IsAuthenticated)
            {
                this.log.Info("The user " + User.Identity.Name + " SingOut succesfully");
                FormsAuthentication.SignOut();
            }
            else
            {
                this.TempData["message"] = string.Format("Вам необходимо сначала войти");
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}
