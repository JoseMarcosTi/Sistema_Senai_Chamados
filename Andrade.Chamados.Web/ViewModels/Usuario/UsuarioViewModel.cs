using Andrade.Chamado.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Andrade.Chamados.Web.ViewModels.Usuario
{
    public class UsuarioViewModel : BaseViewModel
    {
        [Display(Name = "Informe o seu nome")]
        [Required(ErrorMessage = "Informe o campo nome")]
        public string Nome { get; set; }

        [Display(Name = "Informe o seu e-mail")]
        [Required(ErrorMessage = "Informe o campo e-amil")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = " O E-mail é inválido")]
        public string Email { get; set; }

        [Display(Name = "Informe o sua senha")]
        [Required(ErrorMessage = "Informe senha")]
        [DataType(DataType.Password)]
        [MaxLength(8, ErrorMessage = " Número máximo de Caracteres é 8")]
        [MinLength(4, ErrorMessage = " Número minimo de caracteres é 4")]
        public string Senha { get; set; }

        [Display(Name = "Informe o seu telefone")]
        [Required(ErrorMessage = "Informe o campo telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = " Informe o CPF")]
        [MaxLength(14)]
        public string Cpf { get; set; }

        public SelectList ListaSexo { get; set; }
        //[Required(ErrorMessage = "Informe o sexo")]
        public EnSexo Sexo { get; set; }

        public EnTipoUsuario TipoUsuario { get; set; }

        [MaxLength(9, ErrorMessage = " O CEP deve conter 11 números")]
        public string Cep { get; set; }

        public string Logradouro { get; set; }


        public string Numero { get; set; }


        public string Complemento { get; set; }


        public string Bairro { get; set; }


        public string Cidade { get; set; }


        public string Estado { get; set; }

    }
}