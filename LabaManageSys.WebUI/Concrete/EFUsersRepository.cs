using System.Collections.Generic;
using System.Linq;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Concrete
{
    public class EFUsersRepository : IUsersRepository
    {
        private IEFDbContext context;

        public EFUsersRepository(IEFDbContext cont)
        {
            this.context = cont;
        }

        public IEnumerable<UserModel> UserModels
        {
            get
            {
                return this.context.AppUsers.Select(_ => new UserModel
                {
                    UserId = _.UserId,
                    Email = _.Email,
                    Name = _.Name,
                    RoleId = _.RoleId
                }).ToList();
            }
        }

        public IEnumerable<RoleModel> RoleModels
        {
            get
            {
                return this.context.Roles.Select(_ => new RoleModel
                {
                    RoleId = _.RoleId,
                    Name = _.Name
                }).ToList();
            }
        }

        public UserModel UserDelete(int id)
        {
            AppUser entryDb = this.context.AppUsers.Find(id);
            if (entryDb != null)
            {
                this.context.AppUsers.Remove(entryDb);
                this.context.SaveChanges();
            }

            return new UserModel(entryDb);
        }

        public void UserUpdate(UserModel user)
        {
            if (user.UserId == 0)
            {
                this.context.AppUsers.Add(new AppUser
                {
                    Email = user.Email,
                    Name = user.Name,
                    RoleId = user.RoleId,
                    Password = "1"
                });
            }
            else
            {
                AppUser entryDb = this.context.AppUsers.Find(user.UserId);
                if (entryDb != null)
                {
                    entryDb.Name = user.Name;
                    entryDb.Email = user.Email;
                    entryDb.RoleId = user.RoleId;
                    entryDb.Password = entryDb.Password;
                }
            }

            this.context.SaveChanges();
        }

        public bool UserPasswordValidate(UserModel user, string password)
        {
            return this.context.AppUsers.Any(_ => _.UserId == user.UserId && _.Password == password);
        }

        public void UserPasswordSet(UserModel user, string password)
        {
            AppUser entryDb = this.context.AppUsers.FirstOrDefault(_ => _.Name == user.Name);
            if (entryDb != null)
            {
                entryDb.Password = password;
            }

            this.context.SaveChanges();
        }

        public RoleModel GetRoleById(int id)
        {
            var role = this.context.Roles.FirstOrDefault(_ => _.RoleId == id);
            return (role != null) ? new RoleModel(role) : null;
        }

        public RoleModel GetRoleByName(string name)
        {
            var role = this.context.Roles.FirstOrDefault(_ => _.Name == name);
            return (role != null) ? new RoleModel(role) : null;
        }

        public RoleModel GetFirstRole()
        {
            return new RoleModel(this.context.Roles.FirstOrDefault());
        }

        public RoleModel RoleDelete(int id)
        {
            Role entryDb = this.context.Roles.Find(id);
            if (entryDb != null)
            {
                this.context.Roles.Remove(entryDb);
                this.context.SaveChanges();
            }

            return new RoleModel(entryDb);
        }

        public void RoleUpdate(RoleModel role)
        {
            if (role.RoleId == 0)
            {
                this.context.Roles.Add(new Role { Name = role.Name });
            }
            else
            {
                Role entryDb = this.context.Roles.Find(role.RoleId);
                if (entryDb != null)
                {
                    entryDb.Name = role.Name;
                }
            }

            this.context.SaveChanges();
        }

        public IEnumerable<UserModel> GetUserList(int page, int pageSize)
        {
            return this.context.AppUsers.Select(_ => new UserModel
            {
                UserId = _.UserId,
                Email = _.Email,
                Name = _.Name,
                RoleId = _.RoleId
            }).OrderBy(_ => _.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize).ToList();
        }

        public UserModel GetUserByName(string name)
        {
            var user = this.context.AppUsers.FirstOrDefault(_ => _.Email == name || _.Name == name);
            return (user != null) ? new UserModel(user) : null;
        }

        public UserModel GetUserById(int id)
        {
            var user = this.context.AppUsers.FirstOrDefault(_ => _.UserId == id);
            return (user != null) ? new UserModel(user) : null;
        }

        public bool AreUsersInRole(int id)
        {
            return this.context.AppUsers.Any(_ => _.RoleId == id);
        }

        public int GetUsersCount()
        {
           return this.context.AppUsers.Count();
        }

        public string[] GetRolesForUser(string username)
        {
            return new string[] { this.GetRoleById(this.GetUserByName(username).RoleId).Name };
        }

        public bool IsUserInRole(string username, string roleName)
        {
            return this.GetRoleById(this.GetUserByName(username).RoleId).Name == roleName;
        }

        public IEnumerable<UserModel> GetUsersInRole(string roleName)
        {
            var role = this.GetRoleByName(roleName);
            return this.context.AppUsers.Where(_ => _.RoleId == role.RoleId)
                .Select(_ => new UserModel
                {
                    UserId = _.UserId,
                    Name = _.Name,
                    Email = _.Email,
                    RoleId = _.RoleId
                }).ToList();
        }
    }
}