using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamados.Data.Repositorios;
using Andrade.Chamados.Web.ViewModels.Usuario;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Andrade.Chamados.Web.Controllers
{
    [Authorize(Roles ="Administrador")]
    public class UsuarioController : Controller
    {
        // GET: Usuario
      
        public ActionResult Index()
        {
            if (!User.IsInRole("Administrador"))
            {
                ViewBag.Erro = " Você não tem permissão para acessar está tela";
                return View();
            }
            ListaUsuarioViewModel vmListaUsuario = new ListaUsuarioViewModel();

            using (UsuarioRepositorio repUsuario = new UsuarioRepositorio())
            {
                vmListaUsuario.ListaUsuarios = Mapper.Map<List<UsuarioDomain>, List<UsuarioViewModel>>(repUsuario.Listar());
            }

            return View(vmListaUsuario);
        }

        [HttpGet]
        public ActionResult Editar(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["Erro"] = "Informe o id do usuário";
                return RedirectToAction("Index");
            }

            UsuarioViewModel vmUsuario = new UsuarioViewModel();

            using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                vmUsuario = Mapper.Map<UsuarioDomain, UsuarioViewModel>(_repUsuario.BuscarPorId(id));

                if (vmUsuario == null)
                {
                    TempData["Erro"] = "Usuário não encontrado";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(vmUsuario);
                }
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = " Usuário inválido";
                return View();
            }

            try
            {
                //usuario.Telefone = usuario.Telefone.Replace("(", "").Replace(")", "").Replace("-", "").Trim();
                usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-", "").Trim();
                usuario.Cep = usuario.Cep.Replace("-", "").Trim();

                using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
                {
                    _repUsuario.Alterar(Mapper.Map<UsuarioViewModel, UsuarioDomain>(usuario));

                }

                TempData["Erro"] = "Usuário editado";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.Erro = ex.Message;
                return View(usuario);
            }
        }


        [HttpGet]
        public ActionResult Deletar(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["Erro"] = " Informe o id do usuário";
                return RedirectToAction("Index");
            }

            using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                UsuarioViewModel vmUsuario = Mapper.Map<UsuarioDomain, UsuarioViewModel>(_repUsuario.BuscarPorId(id));

                if (vmUsuario == null)
                {
                    TempData["Erro"] = "Usuário não encontrado";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(vmUsuario);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(UsuarioViewModel usuario)
        {
            if (usuario.Id == Guid.Empty)
            {
                TempData["Erro"] = " Informe o id do usuário";
                return RedirectToAction("Index");
            }

            using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                UsuarioViewModel vmUsuario = Mapper.Map<UsuarioDomain, UsuarioViewModel>(_repUsuario.BuscarPorId(usuario.Id));

                if (vmUsuario == null)
                {
                    TempData["Erro"] = "Usuário não encontrado";
                    return RedirectToAction("Index");
                }
                else
                {
                    _repUsuario.Deletar(Mapper.Map<UsuarioViewModel, UsuarioDomain>(vmUsuario));
                    TempData["Erro"] = "Usuário excluído";
                    return RedirectToAction("Index");
                }
            }
        }


    }
}