using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabaManageSys.Domain.EntitiesModel
{
    public class Lesson
    {
        public Lesson()
        {
            this.AppUsers = new HashSet<AppUser>();
        }

        public int LessonId { get; set; }

        public int CourseId { get; set; }

        public DateTime Date { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
