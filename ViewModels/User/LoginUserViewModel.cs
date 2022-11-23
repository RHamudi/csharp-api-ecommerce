using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_ecommerce_dotnet.ViewModels.User
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O Email é obrigatorio")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatoria")]
        public string Password { get; set; }
    }
}