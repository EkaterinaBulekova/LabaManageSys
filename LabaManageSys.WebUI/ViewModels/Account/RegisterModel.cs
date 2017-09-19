using System.ComponentModel.DataAnnotations;

namespace LabaManageSys.WebUI.ViewModels.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Введите пожалуйста имя пользователя")]
        [Display(Name = "Имя пользователя")]
        [StringLength(50, ErrorMessage = "Строка гн должна превышать 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста email пользователя")]
        [Display(Name = "Email пользователя")]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "Строка не должна превышать 50 символов")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}