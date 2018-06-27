using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamados.Data.Contexto;
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

            AndradeChamadosDbContext context = new AndradeChamadosDbContext();
            UsuarioDomain usuarioBanco = new UsuarioDomain();

            try
            {
                usuarioBanco.Id = Guid.NewGuid();
                usuarioBanco.Nome = usuario.Nome;
                usuarioBanco.Email = usuario.Email;
                usuarioBanco.Senha = usuario.Senha;
                usuarioBanco.Telefone = usuario.Telefone.Replace("(","").Replace(")", "").Replace("-", "").Trim();
                usuarioBanco.Cpf = usuario.Cpf.Replace(".", "").Replace("-","").Trim();
                usuarioBanco.Cep = usuario.Cep.Replace("-","").Trim();
                usuarioBanco.Logradouro = usuario.Logradouro;
                usuarioBanco.Complemento = usuario.Complemento;
                usuarioBanco.Bairro = usuario.Bairro;
                usuarioBanco.Cidade = usuario.Cidade;
                usuarioBanco.Estado = usuario.Estado;
                usuarioBanco.DataCriacao = DateTime.Now;
                usuarioBanco.DataAlteracao = DateTime.Now;

                
                context.Usuarios.Add(usuarioBanco);
                context.SaveChanges();

                TempData["Mensagem"] = " Usuário Cadastrado";
                return RedirectToAction("Login");
            }
            catch (System.Exception ex)
            {

                ViewBag.Erro = ex.Message;
                return View(usuario);

            }
            finally
            {
                context = null;
                usuarioBanco = null;
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