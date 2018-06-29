using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamados.Data.Contexto;
using Andrade.Chamados.Data.Repositorios;
using Andrade.Chamados.Web.Models;
using Andrade.Chamados.Web.ViewModels;
using AutoMapper;
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

            using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                UsuarioDomain usuarioDomain = _repUsuario.Login(login.Email, login.Senha);

                if (usuarioDomain != null)
                {
                    return RedirectToAction("Index", "Usuario");
                }
                else
                {
                    ViewBag.Erro = " Usuário ou senha inválidos. Tente novamente";
                    return View(login);
                }
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

            try
            {
                usuario.Telefone = usuario.Telefone.Replace("(","").Replace(")", "").Replace("-", "").Trim();
                usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-","").Trim();
                usuario.Cep = usuario.Cep.Replace("-","").Trim();
                               
                using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
                {
                    _repUsuario.Inserir(Mapper.Map<CadastrarUsuarioViewModel, UsuarioDomain>(usuario));
                }

                TempData["Mensagem"] = " Usuário Cadastrado";
                return RedirectToAction("Login");
            }
            catch (System.Exception ex)
            {

                ViewBag.Erro = ex.Message;
                return View(usuario);

            }    

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