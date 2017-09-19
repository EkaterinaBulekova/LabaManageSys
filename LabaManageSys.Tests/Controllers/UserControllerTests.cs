using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Controllers;
using LabaManageSys.WebUI.HtmlHelpers;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabaManageSys.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void ListTest()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            mock.Setup(m => m.UserModels).Returns(new List<UserModel>
            {
                new UserModel { UserId = 1, Name = "User1" },
                new UserModel { UserId = 2, Name = "User2" },
                new UserModel { UserId = 3, Name = "User3" },
                new UserModel { UserId = 4, Name = "User4" },
                new UserModel { UserId = 5, Name = "User5" },
                new UserModel { UserId = 6, Name = "User6" },
                new UserModel { UserId = 7, Name = "User7" },
                new UserModel { UserId = 8, Name = "User8" },
                new UserModel { UserId = 9, Name = "User9" },
            });
           UserController controller = new UserController(mock.Object, mockLogger.Object);

            // Действие 
            ListViewModel result = (ListViewModel)controller.List(page: 2).Model;

            // Утверждение 
            List<UserModel> users = result.Users.ToList();
            Assert.IsTrue(users.Count == 4);
            Assert.AreEqual(users[0].Name, "User6");
            Assert.AreEqual(users[1].Name, "User7");
            Assert.AreEqual(users[2].Name, "User8");
            Assert.AreEqual(users[3].Name, "User9");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            mock.Setup(m => m.UserModels).Returns(new List<UserModel>
            {
                new UserModel { UserId = 1, Name = "User1" },
                new UserModel { UserId = 2, Name = "User2" },
                new UserModel { UserId = 3, Name = "User3" },
                new UserModel { UserId = 4, Name = "User4" },
                new UserModel { UserId = 5, Name = "User5" },
                new UserModel { UserId = 6, Name = "User6" },
                new UserModel { UserId = 7, Name = "User7" },
                new UserModel { UserId = 8, Name = "User8" },
                new UserModel { UserId = 9, Name = "User9" },
            });
            UserController controller = new UserController(mock.Object, mockLogger.Object);

            // Действие
            ListViewModel result = (ListViewModel)controller.List(page: 2).Model;

            // Утверждение 
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 5);
            Assert.AreEqual(pageInfo.TotalItems, 9);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Edit_User()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            mock.Setup(m => m.UserModels).Returns(new List<UserModel>
            {
                new UserModel { UserId = 1, Name = "User1" },
                new UserModel { UserId = 2, Name = "User2" },
                new UserModel { UserId = 3, Name = "User3" },
                new UserModel { UserId = 4, Name = "User4" },
                new UserModel { UserId = 5, Name = "User5" },
                new UserModel { UserId = 6, Name = "User6" },
                new UserModel { UserId = 7, Name = "User7" },
                new UserModel { UserId = 8, Name = "User8" },
                new UserModel { UserId = 9, Name = "User9" },
            });
            UserController controller = new UserController(mock.Object, mockLogger.Object);

            // Дейтвие
            UserModel userModel1 = (controller.Edit(1).ViewData.Model as EditViewModel).User;
            UserModel userModel2 = (controller.Edit(2).ViewData.Model as EditViewModel).User;
            UserModel userModel3 = (controller.Edit(3).ViewData.Model as EditViewModel).User;

            // Утверждение
            Assert.AreEqual(userModel1.UserId, 1);
            Assert.AreEqual(userModel2.UserId, 2);
            Assert.AreEqual(userModel3.UserId, 3);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();
            Mock<ILogger> mockLogger = new Mock<ILogger>();

            // Организация - создание контроллера
            UserController controller = new UserController(mock.Object, mockLogger.Object);

            // Организация - создание объекта UserModel
            UserModel user = new UserModel { Name = "Test" };

            // Действие - попытка сохранения пользователя
            ActionResult result = controller.Edit(user);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.UserUpdate(user));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();
            Mock<ILogger> mockLogger = new Mock<ILogger>();

            // Организация - создание контроллера
            UserController controller = new UserController(mock.Object, mockLogger.Object);

            // Организация - создание объекта UserModel
            UserModel user = new UserModel { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения пользователя
            ActionResult result = controller.Edit(user);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.UserUpdate(It.IsAny<UserModel>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Users()
        {
            // Организация - создание объекта UserModel
            UserModel user = new UserModel { UserId = 2, Name = "User2" };
            Mock<ILogger> mockLogger = new Mock<ILogger>();

            // Организация - создание имитированного хранилища данных
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.UserModels).Returns(new List<UserModel>
            {
                new UserModel { UserId = 1, Name = "User1" },
                new UserModel { UserId = 2, Name = "User2" },
                new UserModel { UserId = 3, Name = "User3" },
                new UserModel { UserId = 4, Name = "User4" },
                new UserModel { UserId = 5, Name = "User5" },
                new UserModel { UserId = 6, Name = "User6" },
                new UserModel { UserId = 7, Name = "User7" },
                new UserModel { UserId = 8, Name = "User8" },
                new UserModel { UserId = 9, Name = "User9" },
            });

            // Организация - создание контроллера
            UserController controller = new UserController(mock.Object, mockLogger.Object);

            controller.Delete(user.UserId);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта UserModel
            mock.Verify(m => m.UserDelete(user.UserId));
        }
    }
}
