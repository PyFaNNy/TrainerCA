using System.ComponentModel.DataAnnotations;

namespace Trainer.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
