using System.Data.Entity;
using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.Domain.Abstract
{
    public interface IEFDbContext
    {
        IDbSet<AppUser> AppUsers { get; set; }

        IDbSet<Role> Roles { get; set; }

        IDbSet<Lesson> Lessons { get; set; }

        IDbSet<Course> Courses { get; set; }

        IDbSet<Task> Tasks { get; set; }

        IDbSet<Rating> Ratings { get; set; }

        int SaveChanges();
    }
}
