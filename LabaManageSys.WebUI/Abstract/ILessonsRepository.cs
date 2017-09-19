using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Abstract
{
    public interface ILessonsRepository
    {
        IEnumerable<CourseModel> Courses { get; }

        IEnumerable<LessonModel> Lessons { get; }

        int LessonsCountByCourse(int courseId);

        IEnumerable<LessonModel> GetLessonsByCourse(int courseId, int page, int pageSize);

        CourseModel CourseDelete(int courseId);

        CourseModel GetCourseById(int courseId);

        IEnumerable<DateTime> GetDatesByCourse(int courseId);

        void CourseUpdate(CourseModel course, IEnumerable<DateTime> dates);

        void AddUserMissLesson(UserLesson userLesson);

        void RemoveUserMissLesson(UserLesson userLesson);
    }
}
