using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Concrete;
using LabaManageSys.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabaManageSys.Tests.Concrete
{
    [TestClass]
    public class EFRepositoryTests
    {
        [TestMethod]
        public void GetUserListTest()
        {
            // Организация - создание имитированного context для хранилища данных
            var baseUsers = new List<AppUser>
            {
                new AppUser() { UserId = 1, Name = "User1" },
                new AppUser() { UserId = 2, Name = "User2" },
                new AppUser() { UserId = 3, Name = "User3" },
                new AppUser() { UserId = 4, Name = "User4" },
                new AppUser() { UserId = 5, Name = "User5" },
                new AppUser() { UserId = 6, Name = "User6" },
                new AppUser() { UserId = 7, Name = "User7" },
                new AppUser() { UserId = 8, Name = "User8" },
                new AppUser() { UserId = 9, Name = "User9" }
            }.AsQueryable();

            var usersMock = new Mock<IDbSet<AppUser>>();
            usersMock.As<IQueryable<AppUser>>().Setup(m => m.Provider).Returns(baseUsers.Provider);
            usersMock.As<IQueryable<AppUser>>().Setup(m => m.Expression).Returns(baseUsers.Expression);
            usersMock.As<IQueryable<AppUser>>().Setup(m => m.ElementType).Returns(baseUsers.ElementType);
            usersMock.As<IQueryable<AppUser>>().Setup(m => m.GetEnumerator()).Returns(baseUsers.GetEnumerator());
            Mock<IEFDbContext> mock = new Mock<IEFDbContext>();
            
            // mock.Setup(m => m.AppUsers).Returns();
            IUsersRepository repository = new EFUsersRepository(mock.Object);
            
            // Действие 
            IEnumerable<UserModel> result = repository.GetUserList(2, 5);

            // Утверждение 
            List<UserModel> users = result.ToList();
            Assert.IsTrue(users.Count == 4);
            Assert.AreEqual(users[0].Name, "User6");
            Assert.AreEqual(users[1].Name, "User7");
            Assert.AreEqual(users[2].Name, "User8");
            Assert.AreEqual(users[3].Name, "User9");
        }
    }
}
