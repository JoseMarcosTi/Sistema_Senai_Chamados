using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Andrade.Chamados.Web.ViewModels.Conta
{
    public class AlterarSenhaViewModel
    {

        [Required(ErrorMessage = " Informe a senha atual")]
        [DataType(DataType.Password)]
        [Display(Name = " Senha Atual ")]
        [MinLength(4, ErrorMessage =" A senha deve conter pelo menos 4 caracteres")]
        public string SenhaAtual{get; set;}


        [Required(ErrorMessage = " Informe a nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = " Confirmar senha")]
        [MinLength(4, ErrorMessage =" A senha deve contar pelo menos 4 caracteres")]
        public string NovaSenha { get; set; }


        [Required(ErrorMessage =" Informe a nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = " Confirmar senha")]
        [MinLength(4, ErrorMessage = " A senha deve conter pelo menos 4 caracteres")]
        [Compare(nameof(NovaSenha), ErrorMessage =" As senha e confirmação de senha não são iguais")]
        public string ConfirmarSenha { get; set; }

    }
}