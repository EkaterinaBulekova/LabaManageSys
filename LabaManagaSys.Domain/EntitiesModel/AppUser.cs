using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabaManageSys.Domain.EntitiesModel
{
    public class AppUser
    {
        public AppUser()
        {
            this.Lessons = new HashSet<Lesson>();
            this.Ratings = new HashSet<Rating>();
        }

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста имя пользователя")]
        [Display(Name = "Имя пользователя")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста email пользователя")]
        [Display(Name = "Email пользователя")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Пароль пользователя")]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}