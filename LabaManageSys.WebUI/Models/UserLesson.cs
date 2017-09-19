using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabaManageSys.WebUI.Models
{
    public class UserLesson
    {
        public UserLesson()
        {
        }

        public UserLesson(string lessonId, string userId)
        {
            this.LessonId = int.Parse(lessonId);
            this.UserId = int.Parse(userId);
        }

        public int LessonId { get; set; }

        public int UserId { get; set; }
    }
}