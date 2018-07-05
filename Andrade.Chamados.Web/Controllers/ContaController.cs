using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamado.Domain.Enum;
using Andrade.Chamados.Data.Contexto;
using Andrade.Chamados.Data.Repositorios;
using Andrade.Chamados.Web.Models;
using Andrade.Chamados.Web.ViewModels;
using Andrade.Chamados.Web.ViewModels.Usuario;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [ValidateAntiForgeryToken]
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
                    var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, usuarioDomain.Nome),
                        new Claim(ClaimTypes.Email, usuarioDomain.Email),
                        new Claim(ClaimTypes.NameIdentifier, usuarioDomain.Id.ToString()),
                        new Claim(ClaimTypes.Role, usuarioDomain.TipoUsuario.ToString()),
                        new Claim("Telefone", usuarioDomain.Telefone.ToString())
                    }, "ApplicationCookie");

                    Request.GetOwinContext().Authentication.SignIn(identities: identity);
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
            UsuarioViewModel cadastrarUsuario = new UsuarioViewModel();
            // cadastrarUsuario.Nome = " Marcos Andrade";
            // cadastrarUsuario.Email = " andrade.ti@outlook.com";

            cadastrarUsuario.ListaSexo = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem{ Text = "Masculino", Value = "1"},
                    new SelectListItem{ Text = "Feminino", Value = "2"},
                }, "Value", "Text");

            return View(cadastrarUsuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarUsuario(UsuarioViewModel usuario)
        {
            usuario.ListaSexo = ListarSexo();

            if (!ModelState.IsValid)
            {
                ViewBag.Erro = " Dados invalidos";
                return View(usuario);
            }

            try
            {
                //usuario.Telefone = usuario.Telefone.Replace("(","").Replace(")", "").Replace("-", "").Trim();
                usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-","").Trim();
                usuario.Cep = usuario.Cep.Replace("-","").Trim();
                usuario.TipoUsuario = EnTipoUsuario.Padrao;
                               
                using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
                {
                    _repUsuario.Inserir(Mapper.Map<UsuarioViewModel, UsuarioDomain>(usuario));
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


        [HttpGet]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");

            return RedirectToAction("Login");
        }



    }
}