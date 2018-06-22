using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Andrade.Chamados.Web.ViewModels
{
    public class LoginViewModel
    {
       
        [Required(ErrorMessage = "Informe um e-mail válido")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = " O E-mail é inválido")]
        public string Email { get; set; }

       
        [Required(ErrorMessage = "Senha incorreta, digite novamente")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}