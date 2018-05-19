using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Models.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email obrigatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Senha obrigatoria")]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
