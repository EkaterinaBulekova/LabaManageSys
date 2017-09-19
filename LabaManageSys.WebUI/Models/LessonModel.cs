using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.WebUI.Models
{
    public class LessonModel
    {
        public LessonModel()
        {
        }

        public LessonModel(Lesson lesson)
        {
            this.LessonId = lesson.LessonId;
            this.CourseId = lesson.CourseId;
            this.Date = lesson.Date;
            this.Users = lesson.AppUsers.Select(_ => new UserModel(_)).ToList();
        }

        public int LessonId { get; set; }

        public int CourseId { get; set; }

        public DateTime Date { get; set; }
        
        public IEnumerable<UserModel> Users { get; set; }
    }
}