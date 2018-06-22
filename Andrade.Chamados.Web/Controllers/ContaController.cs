using Andrade.Chamados.Web.Models;
using Andrade.Chamados.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Andrade.Chamados.Web.Controllers
{
    public class ContaController : Controller
    {
        // GET: Conta
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Mensagem = " Welcome user! ";
            return View();
        }

        [HttpPost]
        public  ActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = " Usuário inválido";
                return View();
            }
            return View();
        }

        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
            ViewBag.Mensagem = " You're already cadastrated! ";
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = " Dados invalidos";
                return View();
            }

            //TODO: efetuar cadastro banco de dados
            return View();
        }
    }
}