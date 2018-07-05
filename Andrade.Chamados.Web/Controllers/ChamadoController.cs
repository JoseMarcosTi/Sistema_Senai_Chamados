using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamados.Data.Repositorios;
using Andrade.Chamados.Web.ViewModels.Chamado;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Andrade.Chamados.Web.Controllers
{
    [Authorize]
    public class ChamadoController : Controller
    {
        // GET: Chamado
        [HttpGet]
        public ActionResult Index()
        {
            ListaChamadoViewModel vmListaChamados = new ListaChamadoViewModel();

            using (ChamadoRepositorio _repChamado = new ChamadoRepositorio())
            {
                if (User.IsInRole("Administrador"))
                {
                    vmListaChamados.ListaChamados = Mapper.Map<List<ChamadoDomain>, List<ChamadoViewModel>>(_repChamado.Listar());
                }
                else
                {
                    var claims = User.Identity as ClaimsIdentity;
                    var id = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                    vmListaChamados.ListaChamados = Mapper.Map<List<ChamadoDomain>, List<ChamadoViewModel>>(_repChamado.Listar(new Guid(id)));
                }

                return View(vmListaChamados);
            }
        }


    }
}