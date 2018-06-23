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

            // Válida usuário
            if (login.Email == "senai@senai.sp" && login.Senha == "12345")
            {
                TempData["Autenticado"] = " Usuário autenticado";
                // Redireciona para página Home
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Autenticado"] = " Usuário não cadastrado";
                // Redireciona para página de Cadastro de usuário
                return RedirectToAction("CadastrarUsuario");
            }
        }

        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
            CadastrarUsuarioViewModel cadastrarUsuario = new CadastrarUsuarioViewModel();
            // cadastrarUsuario.Nome = " Marcos Andrade";
            // cadastrarUsuario.Email = " andrade.ti@outlook.com";

            cadastrarUsuario.Sexo = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem{ Text = "Masculino", Value = "1"},
                    new SelectListItem{ Text = "Feminino", Value = "2"},
                }, "Value", "Text");

            return View(cadastrarUsuario);
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel usuario)
        {
            usuario.Sexo = ListarSexo();

            if (!ModelState.IsValid)
            {
                ViewBag.Erro = " Dados invalidos";
                return View(usuario);
            }

           //TODO: efetuar cadastro banco de dados
            return View(usuario);
        }

        private SelectList ListarSexo()
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem{ Text = "Masculino", Value = "1"},
                    new SelectListItem{ Text = "Feminino", Value = "2"},
                }, "Value", "Text");
        }
    }
}