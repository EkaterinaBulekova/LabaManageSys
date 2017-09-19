using System.ComponentModel.DataAnnotations;
using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.WebUI.Models
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(AppUser user)
        {
            this.UserId = user.UserId;
            this.Email = user.Email;
            this.Name = user.Name;
            this.RoleId = user.RoleId;
        }

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста имя пользователя")]
        [Display(Name = "Имя пользователя")]
        [StringLength(50, ErrorMessage = "Строка гн должна превышать 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста email пользователя")]
        [Display(Name = "Email пользователя")]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "Строка не должна превышать 50 символов")]
        public string Email { get; set; }

        public int RoleId { get; set; }
    }
}