using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamado.Domain.Enum;
using Andrade.Chamados.Data.Repositorios;
using Andrade.Chamados.Web.ViewModels.Chamado;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Andrade.Chamados.Web.Controllers
{
    //[Authorize]
    public class GraficosController : Controller
    {
        // GET: Graficos
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetDataStatus()
        {
            try
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
                }

                var grupoStatus = vmListaChamados.ListaChamados
                                                    .GroupBy(x => x.Status)
                                                    .Select(n => new
                                                    {
                                                        Status = (EnStatus)n.Key,
                                                        Quantidade = n.Count()
                                                    }).OrderBy(n => n.Quantidade);

                return Json(new { sucesso = true, resultado = grupoStatus }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return Json(new { sucesso = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}