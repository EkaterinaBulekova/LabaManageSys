using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.Role;

namespace LabaManageSys.WebUI.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class RoleController : Controller
    {
        private IUsersRepository repository;

        public RoleController(IUsersRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List()
        {
            ListViewModel roles = new ListViewModel { Roles = this.repository.RoleModels };
            return this.View(roles);
        }

        public ViewResult Edit(int roleId)
        {
            EditViewModel viewRole = new EditViewModel { Role = this.repository.GetRoleById(roleId) };
            return this.View(viewRole);
        }

        [HttpPost]
        public ActionResult Edit(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                this.repository.RoleUpdate(role);
                this.TempData["message"] = string.Format("Изменения в роли \"{0}\" были сохранены", role.Name);
                return this.RedirectToAction("List");
            }
            else
            {
                return this.View(role);
            }
        }

        public ViewResult Create()
        {
            return this.View("Edit", new EditViewModel { Role = new RoleModel() });
        }

        [HttpPost]
        public ActionResult Delete(int roleId)
        {
            if (!this.repository.AreUsersInRole(roleId))
            {
                RoleModel deletedRole = this.repository.RoleDelete(roleId);
                if (deletedRole != null)
                {
                    this.TempData["message"] = string.Format("Роль \"{0}\" успешно удалена.", deletedRole.Name);
                }
            }
            else
            {
                this.TempData["message"] = string.Format("Роль \"{0}\" нельзя удалить пока есть пользователи в этой роли.", this.repository.GetRoleById(roleId).Name);
            }

            return this.RedirectToAction("List");
        }
    }
}