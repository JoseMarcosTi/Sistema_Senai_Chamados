using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamado.Domain.Enum;
using Andrade.Chamados.Data.Repositorios;
using Andrade.Chamados.Web.ViewModels.Chamado;
using Andrade.Chamados.Web.ViewModels.Grafico;
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
            try
            {
                ListaGraficoViewModel vmGrafico = new ListaGraficoViewModel();
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

                #region Gráficos status

                var grupoStatus = vmListaChamados.ListaChamados
                                                    .GroupBy(x => x.Status)
                                                    .Select(n => new
                                                    {
                                                        Status = RetornaStatus(n.Key),
                                                        Quantidade = Convert.ToDouble(n.Count())
                                                    }).OrderBy(n => n.Quantidade);


                vmGrafico.GraficoStatus.Labels = grupoStatus.Select(X => X.Status).ToArray();

                vmGrafico.GraficoStatus.Data = grupoStatus.Select(x => x.Quantidade).ToArray();

                #endregion


                #region Gráficos setor

                var grupoSetor = vmListaChamados.ListaChamados
                                                    .GroupBy(x => x.Setor)
                                                    .Select(n => new
                                                    {
                                                        Setor = RetornaSetor(n.Key),
                                                        Quantidade = Convert.ToDouble(n.Count())
                                                    }).OrderBy(n => n.Quantidade);


                vmGrafico.GraficoSetor.Labels = grupoSetor.Select(X => X.Setor).ToArray();

                vmGrafico.GraficoSetor.Data = grupoSetor.Select(x => x.Quantidade).ToArray();

                #endregion

                return View(vmGrafico);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View();
            }
        }

        private string RetornaSetor(EnSetor setor)
        {
            return setor.ToString();
        }

        private string RetornaStatus(EnStatus status)
        {
            switch (status)
            {
                case EnStatus.Aguardando:
                    return "Aguardando";
                case EnStatus.Iniciado:
                    return "Iniciado";
                case EnStatus.Finalizado:
                    return "Finalizado";
            }
            return null;
        }




    }
}