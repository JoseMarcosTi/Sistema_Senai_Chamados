using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamados.Data.Repositorios;
using Andrade.Chamados.Web.ViewModels.Chamado;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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

               
            }

            return View(vmListaChamados);

        }


        [HttpGet]
        public ActionResult Cadastrar()
        {
            ChamadoViewModel vmChamado = new ChamadoViewModel();

            return View(vmChamado);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(ChamadoViewModel chamado)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Erro = " Dados invalidos";
                    return View(chamado);
                }

                using (ChamadoRepositorio _repChamado = new ChamadoRepositorio())
                {
                    var identity = User.Identity as ClaimsIdentity;
                    var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    chamado.IdUsuario = new Guid(id);
                    _repChamado.Inserir(Mapper.Map<ChamadoViewModel, ChamadoDomain>(chamado));
                }

                TempData["Sucesso"] = " Chamado cadastrado...Aguarde!!!";
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View(chamado);
            }
        }
        
        [HttpGet]
        public ActionResult Editar(Guid? id)
        {
            ChamadoViewModel chamado = new ChamadoViewModel();

            try
            {

                if (id == null)
                {
                    TempData["Erro"] = " Id não identificado";
                    return RedirectToAction("Index");
                }

                using (ChamadoRepositorio _repChamado = new ChamadoRepositorio())
                {
                    chamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(_repChamado.BuscarPorId(id.Value));

                    if (chamado == null)
                    {
                        TempData["Erro"] = "Chamado não encontrado";
                        return RedirectToAction("Index");
                    }

                    #region Buscar Id usuário

                    var identity = User.Identity as ClaimsIdentity;
                    var idUsuario = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                    #endregion

                    if (User.IsInRole("Administrador") || idUsuario == chamado.IdUsuario.ToString())
                    {
                        return View(chamado);

                    }
                    else
                    {
                          TempData["Erro"] = " Este chamado pertence a outro usuário";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (System.Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View(chamado);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ChamadoViewModel chamado)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.Erro = " Dados inválidos";
                    return View(chamado);
                }

                using (ChamadoRepositorio _repChamado = new ChamadoRepositorio())
                {
                    _repChamado.Alterar(Mapper.Map<ChamadoViewModel, ChamadoDomain>(chamado));
                    TempData["Sucesso"] = " Chamado alterado";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ViewBag.Erro = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public ActionResult Excluir(Guid? id)
        {
            try
            {
                if (!User.IsInRole("Administrador"))
                {
                    TempData["Erro"] = " Você não tem permissão para excluir este chamado";
                    return RedirectToAction("Index");
                }

                if (id == null)
                {
                    TempData["Erro"] = " Informe o id do chamado";
                    return RedirectToAction("Index");
                }

                using (ChamadoRepositorio _repChamado = new ChamadoRepositorio())
                {
                    ChamadoViewModel chamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(_repChamado.BuscarPorId(id.Value));

                    if (chamado == null)
                    {
                        TempData["Erro"] = "Chamado não encontrado";
                        return RedirectToAction("Index");
                    }

                    #region Buscar Id usuário

                    var identity = User.Identity as ClaimsIdentity;
                    var idUsuario = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                    #endregion

                    if (User.IsInRole("Administrador") || idUsuario == chamado.IdUsuario.ToString())
                    {
                        return View(chamado);
                    }

                    TempData["Erro"] = "Você não possui permissão para excluir este chamado";
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {

                ViewBag.Erro = ex.Message;
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(ChamadoViewModel chamado)
        {
            try
            {
                if (!User.IsInRole("Administrador"))
                {
                    TempData["Erro"] = " Você não tem permissão para excluir este chamado";
                    return RedirectToAction("Index");
                }

                if (chamado.Id == Guid.Empty)
                {
                    TempData["Erro"] = " Informe o id do chamado";
                    return RedirectToAction("Index");
                }

                using (ChamadoRepositorio _repChamado = new ChamadoRepositorio())
                {
                    ChamadoViewModel vmChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(_repChamado.BuscarPorId(chamado.Id));
                    if (vmChamado == null)
                    {
                        TempData["Erro"] = "Chamado não encontrado";
                        return RedirectToAction("Index");
                    }

                    _repChamado.Deletar(Mapper.Map<ChamadoViewModel, ChamadoDomain>(vmChamado));
                    TempData["Sucesso"] = "Chamado excluido";
                    return RedirectToAction("Index");
                     
                }

            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View(chamado);
            }
        }


    }
}