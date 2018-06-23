using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Andrade.Chamados.Web.Models
{
    public class CadastrarUsuarioViewModel
    {
        [Display(Name ="Informe o seu nome")]
        [Required(ErrorMessage = "Informe o campo nome")]
        public string Nome { get; set; }

        [Display(Name = "Informe o seu e-mail")]
        [Required(ErrorMessage ="Informe o campo e-amil")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = " O E-mail é inválido")]
        public string Email { get; set; }

        [Display(Name = "Informe o seu telefone")]
        [Required(ErrorMessage ="Informe o campo telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Display(Name = "Informe o sua senha")]
        [Required(ErrorMessage ="Informe senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }


        public SelectList Sexo { get; set; }
        [Required(ErrorMessage = "Informe o sexo")]
        public string SexoId { get; set; }

    }
}