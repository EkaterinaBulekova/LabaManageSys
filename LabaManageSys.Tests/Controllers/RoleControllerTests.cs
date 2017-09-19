using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Controllers;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.Role;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabaManageSys.Tests
{
    [TestClass]
    public class RoleControllerTests
    {
        [TestMethod]
        public void Can_List_Roles()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.RoleModels).Returns(new List<RoleModel>
            {
                new RoleModel { RoleId = 1, Name = "Role1" },
                new RoleModel { RoleId = 2, Name = "Role2" },
                new RoleModel { RoleId = 3, Name = "Role3" },
                new RoleModel { RoleId = 4, Name = "Role4" },
            });

            // Организация - создание контроллера
            RoleController controller = new RoleController(mock.Object);

            // Действие 
            ListViewModel result = (ListViewModel)controller.List().Model;

            // Утверждение 
            List<RoleModel> roles = result.Roles.ToList();
            Assert.IsTrue(roles.Count == 4);
            Assert.AreEqual(roles[0].Name, "Role1");
            Assert.AreEqual(roles[1].Name, "Role2");
            Assert.AreEqual(roles[2].Name, "Role3");
            Assert.AreEqual(roles[3].Name, "Role4");
        }

        [TestMethod]
        public void Can_Edit_Role()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.RoleModels).Returns(new List<RoleModel>
            {
                new RoleModel { RoleId = 1, Name = "Role1" },
                new RoleModel { RoleId = 2, Name = "Role2" },
                new RoleModel { RoleId = 3, Name = "Role3" },
                new RoleModel { RoleId = 4, Name = "Role4" },
            });

            // Организация - создание контроллера
            RoleController controller = new RoleController(mock.Object);

            // Дейтвие
            RoleModel role1 = (controller.Edit(1).ViewData.Model as EditViewModel).Role;
            RoleModel role2 = (controller.Edit(2).ViewData.Model as EditViewModel).Role;
            RoleModel role3 = (controller.Edit(3).ViewData.Model as EditViewModel).Role;

            // Утверждение
            Assert.AreEqual(role1.RoleId, 1);
            Assert.AreEqual(role2.RoleId, 2);
            Assert.AreEqual(role3.RoleId, 3);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();

            // Организация - создание контроллера
            RoleController controller = new RoleController(mock.Object);

            // Организация - создание объекта RoleModel
            RoleModel role = new RoleModel { Name = "Test" };

            // Действие - попытка сохранения пользователя
            ActionResult result = controller.Edit(role);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.RoleUpdate(role));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();

            // Организация - создание контроллера
            RoleController controller = new RoleController(mock.Object);

            // Организация - создание объекта RoleModel
            RoleModel role = new RoleModel { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения пользователя
            ActionResult result = controller.Edit(role);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.RoleUpdate(It.IsAny<RoleModel>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Roles()
        {
            // Организация - создание объекта RoleModel
            RoleModel role = new RoleModel { RoleId = 2, Name = "Role2" };

            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.RoleModels).Returns(new List<RoleModel>
            {
                new RoleModel { RoleId = 1, Name = "Role1" },
                new RoleModel { RoleId = 2, Name = "Role2" },
                new RoleModel { RoleId = 3, Name = "Role3" },
                new RoleModel { RoleId = 4, Name = "Role4" },
            });

            // Организация - создание контроллера
            RoleController controller = new RoleController(mock.Object);

            controller.Delete(role.RoleId);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта RoleModel
            mock.Verify(m => m.RoleDelete(role.RoleId));
        }
    }
}
