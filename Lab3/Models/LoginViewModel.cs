using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле не може бути пустим")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Поле не може бути пустим")]
        public string Password { get; set; }
    }
}