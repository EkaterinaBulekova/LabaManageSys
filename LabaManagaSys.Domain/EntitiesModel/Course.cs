using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaManageSys.Domain.EntitiesModel
{
    [Table("Courses")]
    public class Course
    {
        public Course()
        {
            this.Lessons = new HashSet<Lesson>();
        }

        [Key]
        public int CourseId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
