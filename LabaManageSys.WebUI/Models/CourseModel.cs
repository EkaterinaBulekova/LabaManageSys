using System.ComponentModel.DataAnnotations;
using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.WebUI.Models
{
    public class CourseModel
    {
        public CourseModel()
        {
        }

        public CourseModel(Course course)
        {
            this.CourseId = course.CourseId;
            this.Name = course.Name;
        }

        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}