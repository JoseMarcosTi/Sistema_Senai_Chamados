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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            ListaUsuarioViewModel vmListaUsuario = new ListaUsuarioViewModel();

            using (UsuarioRepositorio repUsuario = new UsuarioRepositorio())
            {
                vmListaUsuario.ListaUsuarios = Mapper.Map<List<UsuarioDomain>, List<UsuarioViewModel>>(repUsuario.Listar());
            }

            return View(vmListaUsuario);
        }
    }
}