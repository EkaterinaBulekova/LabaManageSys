using System;
using System.Collections.Generic;
using System.Linq;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Concrete
{
    public class EFLessonsRepository : ILessonsRepository
    {
        private IEFDbContext context;

        public EFLessonsRepository(IEFDbContext cont)
        {
            this.context = cont;
        }

        public IEnumerable<CourseModel> Courses
        {
            get
            {
                return this.context.Courses.Select(_ => new CourseModel
                {
                    CourseId = _.CourseId,
                    Name = _.Name
                }).ToList();
            }
        }

        public IEnumerable<LessonModel> Lessons
        {
            get
            {
                return this.context.Lessons.Select(_ => new LessonModel
                {
                    CourseId = _.CourseId,
                    Date = _.Date,
                    LessonId = _.LessonId
                }).ToList();
            }
        }

        public CourseModel GetCourseById(int courseId)
        {
            var course = this.context.Courses.FirstOrDefault(_ => _.CourseId == courseId);
            return (course != null) ? new CourseModel(course) : null;
        }

        public IEnumerable<DateTime> GetDatesByCourse(int courseId)
        {
            return this.context.Lessons
                .Where(_ => _.CourseId == courseId).Select(_ => _.Date).ToList();
        }

        public CourseModel CourseDelete(int courseId)
        {
            Course entryDb = this.context.Courses.Find(courseId);
            if (entryDb != null)
            {
                this.context.Courses.Remove(entryDb);
                this.context.SaveChanges();
            }

            return new CourseModel(entryDb);
        }

        public void CourseUpdate(CourseModel course, IEnumerable<DateTime> dates)
        {
            if (course.CourseId != 0)
            {
                this.CourseDelete(course.CourseId);
            }

            this.context.Courses.Add(new Course
            {
                Name = course.Name,
                Lessons = dates.Select(_ => 
                new Lesson
                {
                    Date = _,
                    CourseId = course.CourseId
                }).ToList()
            });
            this.context.SaveChanges();
        }

        public IEnumerable<LessonModel> GetLessonsByCourse(int courseId, int page, int pageSize)
        {
            return this.context.Lessons.Where(_ => _.CourseId == courseId)
                .Select(_ => 
                new LessonModel
                {
                    CourseId = _.CourseId,
                    Date = _.Date,
                    LessonId = _.LessonId,
                    Users = _.AppUsers.Select(u => 
                    new UserModel
                    {
                        UserId = u.UserId,
                        Email = u.Email,
                        Name = u.Name,
                        RoleId = u.RoleId
                    }).ToList()
                }).OrderBy(_ => _.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public void AddUserMissLesson(UserLesson userLesson)
        {
            Lesson lessonDbentry = this.context.Lessons.FirstOrDefault(_ => _.LessonId == userLesson.LessonId);
            if (lessonDbentry != null)
            {
                AppUser userDbentry = this.context.AppUsers.FirstOrDefault(_ => _.UserId == userLesson.UserId);
                if (userDbentry != null && (!lessonDbentry.AppUsers.Any(_ => _.UserId == userLesson.UserId)))
                {
                    lessonDbentry.AppUsers.Add(userDbentry);
                    this.context.SaveChanges();
                }
            }
        }

        public void RemoveUserMissLesson(UserLesson userLesson)
        {
            Lesson lessonDbentry = this.context.Lessons.FirstOrDefault(_ => _.LessonId == userLesson.LessonId);
            if (lessonDbentry != null)
            {
                AppUser userDbentry = lessonDbentry.AppUsers.FirstOrDefault(_ => _.UserId == userLesson.UserId);
                if (userDbentry != null)
                {
                    lessonDbentry.AppUsers.Remove(userDbentry);
                    this.context.SaveChanges();
                }
            }
        }

        public int LessonsCountByCourse(int courseId)
        {
            return this.Lessons.Where(_ => _.CourseId == courseId).Count();
        }
    }
}