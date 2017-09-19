using System.ComponentModel.DataAnnotations;

namespace LabaManageSys.WebUI.ViewModels.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите пожалуйста имя или Email")]
        [Display(Name = "Имя пользователя или Email")]
        [StringLength(50, ErrorMessage = "Строка не должна превышать 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(50, ErrorMessage = "Строка не должна превышать 50 символов")]
        public string Password { get; set; }
    }
}