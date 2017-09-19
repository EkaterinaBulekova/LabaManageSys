using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabaManageSys.Domain.EntitiesModel
{
    public class Task
    {
        public Task()
        {
            this.Ratings = new HashSet<Rating>();
        }

        [Required]
        public int TaskId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Уровень сложности")]
        public int Level { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Наименование предмета")]
        public string Topic { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Наименование задачи")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Текст задания")]
        public string Text { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
